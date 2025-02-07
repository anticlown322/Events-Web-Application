using Domain.Contracts;
using Domain.Entities.Models;
using Service.Contracts;
using Shared.DTO.Events;

namespace Service.Services;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;

    public EventService(IRepositoryManager repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<EventDto> GetAllEvents(bool trackChanges)
    {
        try
        {
            var events = _repository.Event.GetAllEvents(trackChanges);

            var eventsDto = events.Select(e =>
                    new EventDto(
                        e.Id,
                        e.Name,
                        e.Description,
                        e.StartDate.ToLongDateString(),
                        e.Location,
                        e.Category,
                        e.MaxParticipants,
                        e.Image))
                .ToList();
            return eventsDto;
        }
        catch (Exception ex)
        {
            // add loger later
            throw;
        }
    }
}