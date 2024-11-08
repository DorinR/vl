using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository.Interfaces;

public interface IEventRepository
{
    public Task<string> AddEvent(string thought, Guid userId);

    public Task<List<Event>> RetrieveEvents(Guid userId);

    public Task AddDistortionsToEvent(string eventId, List<DistortionDto> distortionsToAttach);

    public Task<string> DeleteEvent(string eventId);
}