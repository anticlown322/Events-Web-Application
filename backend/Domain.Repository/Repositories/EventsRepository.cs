using Domain.Contracts;
using Domain.Entities.Models;

namespace Domain.Repository.Repositories;

public class EventsRepository : RepositoryBase<Event>, IEventsRepository
{
    public EventsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public IEnumerable<Event> GetAllEvents(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(e => e.Name)
            .ToList();

    public Event GetEvent(Guid eventId, bool trackChanges) =>
        FindByCondition(e => e.Id.Equals(eventId), trackChanges)
            .SingleOrDefault();

    public void CreateEvent(Event eventToCreate) => Create(eventToCreate);

    public IEnumerable<Event> GetEventsByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

    public void DeleteEvent(Event eventToDelete) => Delete(eventToDelete);
}