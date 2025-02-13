using Domain.Contracts;
using Domain.Entities.Models;
using Infrastructure.Repository;
using Infrastructure.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository.Repositories;

public class EventsRepository : RepositoryBase<Event>, IEventsRepository
{
    public EventsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters,
        bool trackChanges)
    {
        var query = FindAll(trackChanges)            
            .OrderBy(e => e.Name)
            .Skip((eventParameters.PageNumber - 1) * eventParameters.PageSize)
            .Take(eventParameters.PageSize);

        if (eventParameters.StartDate.HasValue)
        {
            query = query.Where(e => e.StartDate.Equals(eventParameters.StartDate.Value));
        }
        
        if (!string.IsNullOrEmpty(eventParameters.Location))
        {
            query = query.Where(e => e.Location == eventParameters.Location);
        }

        if (eventParameters.Category.HasValue)
        {
            query = query.Where(e => e.Category == eventParameters.Category.Value);
        }

        var events = await query.ToListAsync();
        var totalCount = await query.CountAsync();

        return new PagedList<Event>(events, totalCount, eventParameters.PageNumber, eventParameters.PageSize);
    }
        
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