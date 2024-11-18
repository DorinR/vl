namespace webapitest.Repository.ArtifactGeneration.Interfaces;

public interface IArtifactGenerationRepository
{
    public Task<string> CreateArtifact(string initialDetails, string artifactType);
}