using webapitest.Business.ArtifactGeneration;

namespace webapitest.Repository.ArtifactGeneration.Interfaces;

public interface IArtifactGenerationRepository
{
    public Task<int> InitializeArtifact(string initialDetails, string artifactType,
        List<FragmentSpec> fragmentSpecs);
}