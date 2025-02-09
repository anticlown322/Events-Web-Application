using Domain.Entities.Models;
using Shared.DTO.Events;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<EventDto> GetAllEvents(bool trackChanges);
    EventDto GetEventById(Guid eventId, bool trackChanges);
    EventDto GetEventByName(string name, bool trackChanges);
    EventDto CreateEvent(EventForCreationDto eventToCreate);
    IEnumerable<EventDto> GetEventsByIds(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteEvent(Guid eventId, bool trackChanges);
    void UpdateEvent(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges);
}