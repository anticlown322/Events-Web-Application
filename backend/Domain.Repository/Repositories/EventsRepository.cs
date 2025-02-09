using Domain.Contracts;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository.Repositories;

public class EventsRepository : RepositoryBase<Event>, IEventsRepository
{
    public EventsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();

    public async Task<Event> GetEventByIdAsync(Guid eventId, bool trackChanges) =>
        await FindByCondition(e => e.Id.Equals(eventId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<Event> GetEventByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(e => e.Name.Equals(name), trackChanges)
            .FirstOrDefaultAsync();

    public void CreateEvent(Event eventToCreate) => Create(eventToCreate);

    public async Task<IEnumerable<Event>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

    public void DeleteEvent(Event eventToDelete) => Delete(eventToDelete);
}