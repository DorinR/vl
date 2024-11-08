using webapitest.Repository.Models;

namespace webapitest.Controllers.Models;

public class RetrieveThoughtsResponseModel
{
    public List<ThoughtDto> Thoughts { get; set; }
}