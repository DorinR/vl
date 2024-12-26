using webapitest.Repository.Fragment.Models;

namespace webapitest.Repository.Fragment.Interfaces;

public interface IFragmentRepository
{
    public Task<List<FragmentModel>> GetArtifactFragments(int artifactId);
    public Task<List<FragmentModel>> GetEmptyArtifactFragments(int artifactId);
}