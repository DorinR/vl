using webapitest.Repository;
using webapitest.Repository.ArtifactGeneration.Interfaces;

namespace webapitest.Business.ArtifactGeneration;

public class ArtifactGenerationBusiness
{
    private readonly LLMChatRepository _llmChatRepository;
    private readonly IArtifactGenerationRepository _artifactGenerationRepository;
    private static readonly List<FragmentSpec> DemandLetterFragments = new List<FragmentSpec>()
    {
        new FragmentSpec()
        {
            Code = "FirstName",
            Description = "The first name of the plaintiff"
        },
        new FragmentSpec()
        {
            Code = "LastName",
            Description = "The last name of the plaintiff"
        },
        new FragmentSpec()
        {
            Code = "WhatHappened",
            Description = "A description of the events that led to the demand"
        },
        new FragmentSpec()
        {
            Code = "Demand",
            Description = "What are the demands that the plaintiff is making"
        },
        new FragmentSpec()
        {
            Code = "FirstName",
            Description = "The first name of the plaintiff"
        }
    };

    public ArtifactGenerationBusiness(LLMChatRepository llmChatRepository, IArtifactGenerationRepository artifactGenerationRepository)
    {
        _llmChatRepository = llmChatRepository;
        _artifactGenerationRepository = artifactGenerationRepository;
    }

    public async Task<string> InitializeDemandLetter(string situationExplanation)
    {
        var response = await _llmChatRepository.GetChatResponseWithSystemMessage(
            situationExplanation,
            "You are an expert legal assistant trained to draft professional demand letters specifically tailored for the province of Quebec. Your task is to produce a clear, concise, and professional demand letter based on the user's provided information. The demand letter must:\n\nClearly identify the recipient and address them respectfully.\nSummarize the user's situation succinctly but comprehensively, outlining the events leading to the demand.\nCite relevant laws, regulations, or legal principles under Quebec civil law that support the user's position if applicable.\nState the specific demands or remedies the user is seeking, such as monetary compensation, corrective action, or other relief.\nSet a clear deadline for compliance and outline the consequences of failing to meet the demands (e.g., legal action).\nUse a tone that is formal yet accessible, firm but not overly aggressive.\nYou will receive the specific details about the user's situation in the user message. Generate the demand letter based on these details, ensuring it is tailored to the legal and cultural context of Quebec.");

        
        
        await _artifactGenerationRepository.CreateArtifact(situationExplanation, "Demand Letter");
        
        return response;
    }

    #region private methods

    

    #endregion

    // public async Task<string> GetNextQuestionOrGetDemandLetter(string demandLetterId)
    // {
    //     // get all fragments
    //     // get first missing fragment
    //     // return question relating to fragment
    //     // if all fragments complete return demand letter
    // }

    // public async Task<string> GetNextQuestionOrGetDemandLetter(string demandLetterId)
    // {
    //     // get all the data we have for that demand letter (initial creation + fragments)
    //     // comapare that to the list of info required
    //         // if some missing return {done: false, followUp: {question}}
    //         // if complete return {done: true, demandLetter}
    //         
    //     
    //
    // }

    class FragmentSpec
    {
        public string Code { get; set; }
        
        public string Description { get; set; }
    }
}