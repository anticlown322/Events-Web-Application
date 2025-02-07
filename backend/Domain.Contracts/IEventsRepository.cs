using Domain.Entities.Models;

namespace Domain.Contracts;

public interface IEventsRepository
{
    IEnumerable<Event> GetAllEvents(bool trackChanges);
}