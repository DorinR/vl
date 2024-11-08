using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository.Interfaces;

public interface IThoughtRepository
{
    public Task<string> AddThought(string thought, Guid userId);

    public Task<List<Thought>> RetrieveThoughts(Guid userId);

    public Task AddDistortionsToThought(string thoughtId, List<DistortionDto> distortionsToAttach);

    public Task<string> DeleteThought(string thoughtId);
}