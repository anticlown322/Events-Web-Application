using Domain.Models;
using Domain.RequestFeatures;

namespace Domain.RepositoryContracts;

public interface IEventsRepository
{
    Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters, bool trackChanges, CancellationToken cancellationToken);
    Task<Event> GetEventByIdAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken);
    Task<Event> GetEventByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken);
    void CreateEvent(Event eventToCreate);
    void DeleteEvent(Event eventToDelete);
}