using Application.Contracts.RepositoryContracts;
using Application.RequestFeatures;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories;

public class EventsRepository : RepositoryBase<Event>, IEventsRepository
{
    public EventsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters,
        bool trackChanges, CancellationToken cancellationToken)
    {
        var events = await FindAllAsync(trackChanges, cancellationToken);
        var query = events.AsQueryable();

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

        var orderedQuery = query.OrderBy(e => e.Name);

        var pagedEvents = orderedQuery
            .Skip((eventParameters.PageNumber - 1) * eventParameters.PageSize)
            .Take(eventParameters.PageSize)
            .ToList();

        var totalCount = orderedQuery.Count();

        return new PagedList<Event>(pagedEvents, totalCount, eventParameters.PageNumber, eventParameters.PageSize);
    }

    public async Task<Event> GetEventByIdAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventToGet = await 
            FindByConditionAsync(e => e.Id.Equals(eventId), trackChanges, cancellationToken);
        return eventToGet.SingleOrDefault();
    }

    public async Task<Event> GetEventByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventToGet = await FindByConditionAsync(e => e.Name.Equals(name), trackChanges, cancellationToken);
        return eventToGet.SingleOrDefault();
    }

    public async Task<IEnumerable<Event>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken)
    {
        var events = await FindByConditionAsync(x => ids.Contains(x.Id), trackChanges, cancellationToken);
        return events.ToList();
    }

    public void CreateEvent(Event eventToCreate) => Create(eventToCreate);

    public void DeleteEvent(Event eventToDelete) => Delete(eventToDelete);
}