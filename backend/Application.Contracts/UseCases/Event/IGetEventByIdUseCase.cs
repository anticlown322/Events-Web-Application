using Application.DTO.Events;

namespace Application.Contracts.UseCases.Event;

public interface IGetEventByIdUseCase
{
    Task<EventDto> ExecuteAsync(Guid eventId, bool trackChanges);
}