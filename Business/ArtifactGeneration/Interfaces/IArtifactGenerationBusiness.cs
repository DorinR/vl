namespace webapitest.Business.ArtifactGeneration.Interfaces;

public interface IArtifactGenerationBusiness
{
    public Task<string> InitializeDemandLetter(string situationExplanation);

    public Task<string> GetNextQuestion(int artifactId);

    public Task<SubmitMoreInformationResponse> SubmitMoreInformation(int artifactId, string answer);

    public Task<string> GenerateArtifact(string artifactId);
}

public record SubmitMoreInformationResponse
{
    public bool isDone { get; init; }
}