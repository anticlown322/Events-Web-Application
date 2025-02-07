using Domain.Entities.Models;
using Shared.DTO.Events;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<EventDto> GetAllEvents(bool trackChanges);

}