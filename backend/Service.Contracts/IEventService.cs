using Domain.Entities.Models;
using Shared.DTO.Events;

namespace Service.Contracts;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync(bool trackChanges);
    Task<EventDto> GetEventByIdAsync(Guid eventId, bool trackChanges);
    Task<EventDto> GetEventByNameAsync(string name, bool trackChanges);
    Task<EventDto> CreateEventAsync(EventForCreationDto eventToCreate);
    Task<IEnumerable<EventDto>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task DeleteEventAsync(Guid eventId, bool trackChanges);
    Task UpdateEventAsync(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges);
}