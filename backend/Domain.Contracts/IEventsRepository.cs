using Domain.Entities.Models;

namespace Domain.Contracts;

public interface IEventsRepository
{
    IEnumerable<Event> GetAllEvents(bool trackChanges);
    Event GetEventById(Guid eventId, bool trackChanges);
    Event GetEventByName(string name, bool trackChanges);
    void CreateEvent(Event eventToCreate);
    IEnumerable<Event> GetEventsByIds(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteEvent(Event eventToDelete);
}