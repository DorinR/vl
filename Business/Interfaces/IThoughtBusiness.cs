using webapitest.Controllers.Models;
using webapitest.Repository.Models;

namespace webapitest.Business.Interfaces;

public interface IThoughtBusiness
{
    public Task<GetThoughtDistortionsResponseModel> GetThoughtDistortions(string thought);

    public Task<string> AddThought(string thought);

    public Task<List<ThoughtDto>> RetrieveThoughts();

    public Task<string> DeleteThought(string thoughtId);
}