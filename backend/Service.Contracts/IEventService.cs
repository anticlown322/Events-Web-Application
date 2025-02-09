using Domain.Entities.Models;
using Shared.DTO.Events;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<EventDto> GetAllEvents(bool trackChanges);
    EventDto GetEvent(Guid eventId, bool trackChanges);
    EventDto CreateEvent(EventForCreationDto eventToCreate);
    IEnumerable<EventDto> GetEventsByIds(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteEvent(Guid eventId, bool trackChanges);
    void UpdateEvent(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges);
}