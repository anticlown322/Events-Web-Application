using Domain.Entities.Models;
using Shared.RequestFeatures;

namespace Domain.Contracts;

public interface IEventsRepository
{
    Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters, bool trackChanges);
    Task<Event> GetEventByIdAsync(Guid eventId, bool trackChanges);
    Task<Event> GetEventByNameAsync(string name, bool trackChanges);
    void CreateEvent(Event eventToCreate);
    Task<IEnumerable<Event>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteEvent(Event eventToDelete);
}