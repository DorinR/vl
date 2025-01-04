namespace webapitest.Business.ArtifactGeneration.Interfaces;

public interface IArtifactGenerationBusiness
{
    public Task<int> InitializeDemandLetter(string situationExplanation);

    public Task<string> GetNextQuestion(int artifactId);

    public Task<SubmitMoreInformationResponse> SubmitMoreInformation(int artifactId, string userAnswer,
        string userQuestion);

    public Task<GenerateArtifactResponse> GenerateArtifact(int artifactId);
}

public record SubmitMoreInformationResponse
{
    public bool isDone { get; init; }
}

public record GenerateArtifactResponse(string Artifact);