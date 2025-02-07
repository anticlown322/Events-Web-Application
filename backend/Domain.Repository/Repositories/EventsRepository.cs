using Domain.Contracts;
using Domain.Entities.Models;

namespace Domain.Repository.Repositories;

public class EventsRepository : RepositoryBase<Event>, IEventsRepository
{
    public EventsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    { }
    
    public IEnumerable<Event> GetAllEvents(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(e => e.Name)
            .ToList();

}