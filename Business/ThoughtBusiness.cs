using webapitest.Business.Interfaces;
using webapitest.Controllers.Models;
using webapitest.Repository;
using webapitest.Repository.Interfaces;
using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Business;

public class ThoughtBusiness : IThoughtBusiness
{
    private readonly DistortionRepository _distortionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LLMChatRepository _llmChatRepository;
    private readonly IThoughtRepository _thoughtRepository;
    private readonly IUserRepository _userRepository;

    public ThoughtBusiness(IThoughtRepository thoughtRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, DistortionRepository distortionRepository,
        LLMChatRepository llmChatRepository)
    {
        _thoughtRepository = thoughtRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _distortionRepository = distortionRepository;
        _llmChatRepository = llmChatRepository;
    }

    public Task<GetThoughtDistortionsResponseModel> GetThoughtDistortions(string thought)
    {
        throw new NotImplementedException();
    }

    public async Task<string> AddThought(string thought)
    {
        var user = await GetUser();

        if (user == null) return null;

        var addedThoughtId = await _thoughtRepository.AddThought(thought, user.Id);

        var availableDistortions = await _distortionRepository.GetAllDistortions();

        var detectedDistortions = await _llmChatRepository.DetectDistortionsInThought(availableDistortions, thought);

        await _thoughtRepository.AddDistortionsToThought(addedThoughtId, detectedDistortions);

        return addedThoughtId;
    }

    public async Task<List<ThoughtDto>> RetrieveThoughts()
    {
        var user = await GetUser();

        if (user == null) return null;

        var response = await _thoughtRepository.RetrieveThoughts(user.Id);

        var mappedResponse = response.Select(thought => new ThoughtDto
        {
            Id = thought.Id,
            DateCreated = thought.DateCreated.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Distortions = thought.Distortions.Select(distortion => new DistortionDto
            {
                Id = distortion.Id.ToString(),
                Name = distortion.Name
            }).ToList(),
            Content = thought.Content
        }).ToList();

        return mappedResponse;
    }

    public async Task<string> DeleteThought(string thoughtId)
    {
        var response = await _thoughtRepository.DeleteThought(thoughtId);

        return response;
    }

    private async Task<User> GetUser()
    {
        var userId = _httpContextAccessor.HttpContext.Items["userId"];

        User? relatedUser = null;
        if (userId != null) relatedUser = await _userRepository.GetUserById(Guid.Parse((string)userId));

        return relatedUser;
    }
}