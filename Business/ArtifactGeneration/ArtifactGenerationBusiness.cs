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
            Description = "The first name of the plaintiff"
        },
        new FragmentSpec
        {
            Code = "LastName",
            Description = "The last name of the plaintiff"
        },
        new FragmentSpec
        {
            Code = "WhatHappened",
            Description = "A description of the events that led to the demand"
        },
        new FragmentSpec
        {
            Code = "Demand",
            Description = "What are the demands that the plaintiff is making"
        },
        new FragmentSpec
        {
            Code = "FirstName",
            Description = "The first name of the plaintiff"
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

    public async Task<string> InitializeDemandLetter(string situationExplanation)
    {
        var response = await _llmChatRepository.GetChatResponseWithSystemMessage(
            situationExplanation,
            "You are an expert legal assistant trained to draft professional demand letters specifically tailored for the province of Quebec. Your task is to produce a clear, concise, and professional demand letter based on the user's provided information. The demand letter must:\n\nClearly identify the recipient and address them respectfully.\nSummarize the user's situation succinctly but comprehensively, outlining the events leading to the demand.\nCite relevant laws, regulations, or legal principles under Quebec civil law that support the user's position if applicable.\nState the specific demands or remedies the user is seeking, such as monetary compensation, corrective action, or other relief.\nSet a clear deadline for compliance and outline the consequences of failing to meet the demands (e.g., legal action).\nUse a tone that is formal yet accessible, firm but not overly aggressive.\nYou will receive the specific details about the user's situation in the user message. Generate the demand letter based on these details, ensuring it is tailored to the legal and cultural context of Quebec.");

        await _artifactGenerationRepository.InitializeArtifact(situationExplanation, "Demand Letter",
            DemandLetterFragments);

        return response;
    }

    public Task<SubmitMoreInformationResponse> SubmitMoreInformation(int artifactId, string answer)
    {
        // get all fragments that are empty
        // fill in each one using answer from user
        // if all have been filled in return {isDone: true} else return {isDone: false}
        throw new NotImplementedException();
    }

    public Task<string> GenerateArtifact(string artifactId)
    {
        // get info from artifact and fragments and generate entire document.
        throw new NotImplementedException();
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
}

public class FragmentSpec
{
    public string Code { get; set; }

    public string Description { get; set; }
}