using webapitest.Repository.Interfaces;
using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository;

public class EventRepository : IEventRepository
{
    public Task<string> AddEvent(string thought, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Event>> RetrieveEvents(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task AddDistortionsToEvent(string eventId, List<DistortionDto> distortionsToAttach)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteEvent(string eventId)
    {
        throw new NotImplementedException();
    }
}