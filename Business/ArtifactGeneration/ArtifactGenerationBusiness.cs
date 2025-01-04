using webapitest.Business.ArtifactGeneration.Interfaces;
using webapitest.Repository;
using webapitest.Repository.ArtifactGeneration.Interfaces;
using webapitest.Repository.Fragment.Interfaces;

namespace webapitest.Business.ArtifactGeneration;

public class ArtifactGenerationBusiness : IArtifactGenerationBusiness
{
    private static readonly List<FragmentSpec> DemandLetterFragments = new()
    {
        new FragmentSpec
        {
            Code = "FirstName",
            Description = "The first name of the plaintiff",
            Value = null
        },
        new FragmentSpec
        {
            Code = "LastName",
            Description = "The last name of the plaintiff",
            Value = null
        },
        new FragmentSpec
        {
            Code = "WhatHappened",
            Description = "A description of the events that led to the demand",
            Value = null
        },
        new FragmentSpec
        {
            Code = "Date",
            Description = "The date on which the events took place",
            Value = null
        },
        new FragmentSpec
        {
            Code = "Accused",
            Description = "The identity of the accused, either their name of their company",
            Value = null
        }
    };

    private readonly IArtifactGenerationRepository _artifactGenerationRepository;
    private readonly IFragmentRepository _fragmentRepository;
    private readonly LLMChatRepository _llmChatRepository;

    public ArtifactGenerationBusiness(LLMChatRepository llmChatRepository,
        IArtifactGenerationRepository artifactGenerationRepository, IFragmentRepository fragmentRepository)
    {
        _llmChatRepository = llmChatRepository;
        _artifactGenerationRepository = artifactGenerationRepository;
        _fragmentRepository = fragmentRepository;
    }

    public async Task<int> InitializeDemandLetter(string situationExplanation)
    {
        var llmTasks = new List<Task<string>>();
        foreach (var fragment in DemandLetterFragments)
        {
            var llmTask = _llmChatRepository.GetChatResponseWithSystemMessage(
                $"{fragment.Description}%{situationExplanation}",
                "I will give you a description of a piece of information that I need in order to write a demand letter, and second will be the initial information that the user just submitted. Your task is to assess whether or not the information submitted by the user satisfies the required piece of information. If it does then return to me a string representing the answer to the piece of information requested (i will store this in the DB so that we can use it later to write up the document) and if not then just return just `NO`. Basically the goal is to check if what the user just told me answers the question (piece of required information for the demand letter) that I had. You must reply with either 'NO' or a string with a string representing the data for that fragment");
            llmTasks.Add(llmTask);
        }

        var llmResponses = await Task.WhenAll(llmTasks);

        var initialFragments = DemandLetterFragments.Zip(llmResponses, (f, r) => new FragmentSpec
        {
            Code = f.Code,
            Description = f.Description,
            Value = r == "NO" ? null : r
        }).ToList();

        var artifactId = await _artifactGenerationRepository.InitializeArtifact(situationExplanation, "Demand Letter",
            initialFragments);

        return artifactId;
    }

    public async Task<string> GetNextQuestion(int artifactId)
    {
        var fragments = await _fragmentRepository.GetEmptyArtifactFragments(artifactId);

        if (fragments == null || !fragments.Any())
            throw new InvalidOperationException("No empty fragments found for the specified artifact.");

        var missingInformationList = fragments.Select(x => x.Description).ToList();
        var missingInformation = string.Join("%", missingInformationList);

        // generate a question that would lead the user to answer the most of these in one go.
        var response = await _llmChatRepository.GetChatResponseWithSystemMessage(missingInformation,
            "I will give you a list of missing informations that is required to complete a demand letter. The pieces of information that are required will be separated by a % sign. You task is to generate a question that I can ask to the user to complete one or more of the missing pieces of information. Like try to ask a question that can knock out a few but its okay if you do one at a time as well. Act as a lawyer speaking to a lay-person");

        // return the question
        return response;
    }

    public async Task<SubmitMoreInformationResponse> SubmitMoreInformation(int artifactId, string userAnswer,
        string userQuestion)
    {
        // get all fragments that are empty
        var emptyFragments =
            await _fragmentRepository.GetEmptyArtifactFragments(artifactId);

        if (emptyFragments == null)
            throw new InvalidOperationException("No empty fragments found for the specified artifact.");

        // fill in each one using answer from user
        var llmTasks = new List<Task<string>>();
        foreach (var emptyFragment in emptyFragments)
        {
            var llmTask = _llmChatRepository.GetChatResponseWithSystemMessage(
                $"{emptyFragment.Description}%{userAnswer}%{userQuestion}",
                "I will give you three things separated by a %. First I will give you a description of a piece of information that I need in order to write a demand letter, and second will be the latest bit of information that the user just submitted and third will be the question that the user was given. Your task is to assess whether or not the information submitted by the user satisfies the required piece of information. If it does then return to me a string representing the answer to the piece of information requested (i will store this in the DB so that we can use it later to write up the document) and if not then just return just `NO`. Basically the goal is to check if what the user just told me answers the question (piece of required information for the demand letter) that I had. Also keep in mind that the user is the plaintiff so if they say their name is John, it means that's the name of the plaintiff");
            llmTasks.Add(llmTask);
        }

        var answers = await Task.WhenAll(llmTasks);

        var numberOfFragmentsCompleted = 0;

        // update the DB with the answers
        foreach (var (answer, fragment) in answers.Zip(emptyFragments, (a, f) => (a, f)))
            if (!answer.ToUpper().Contains("NO"))
            {
                await _fragmentRepository.AddFragmentValue(fragment.Id, answer);
                numberOfFragmentsCompleted++;
            }

        // return whether all fragments have been completed
        if (numberOfFragmentsCompleted == emptyFragments.Count)
            return new SubmitMoreInformationResponse
            {
                isDone = true
            };

        return new SubmitMoreInformationResponse
        {
            isDone = false
        };
    }

    public async Task<GenerateArtifactResponse> GenerateArtifact(int artifactId)
    {
        // get info from artifact and fragments and generate entire document.
        var fragments = await _fragmentRepository.GetArtifactFragments(artifactId);
        var allArtifactInfo = fragments.Select(x => $"{x.Description}:{x.Value}").ToList();
        var allArtifactInfoString = string.Join("%", allArtifactInfo);

        // generate the entire document.
        var response = await _llmChatRepository.GetChatResponseWithSystemMessage(allArtifactInfoString,
            "You are an expert legal assistant trained to draft professional demand letters specifically tailored for the province of Quebec. Your task is to produce a clear, concise, and professional demand letter based on the user's provided information. The demand letter must:\\n\\nClearly identify the recipient and address them respectfully.\\nSummarize the user's situation succinctly but comprehensively, outlining the events leading to the demand. Cite relevant laws, regulations, or legal principles under Quebec civil law that support the user's position if applicable. State the specific demands or remedies the user is seeking, such as monetary compensation, corrective action, or other relief. Set a clear deadline for compliance and outline the consequences of failing to meet the demands (e.g., legal action). Use a tone that is formal yet accessible, firm but not overly aggressive. You will receive the specific details about the user's situation in the user message. Generate the demand letter based on these details, ensuring it is tailored to the legal and cultural context of Quebec. Here are all the pieces of information that you need in order to write a demand letter. The pieces of information will be of the form description:answer and will be separated by % signs.");

        return new GenerateArtifactResponse(response);
    }
}

public class FragmentSpec
{
    public string Code { get; set; }

    public string Description { get; set; }

    public string? Value { get; set; }
}