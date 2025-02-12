using Shared.DTO.Events;

namespace Service.Contracts.UseCases.Event;

public interface IGetEventByIdUseCase
{
    Task<EventDto> ExecuteAsync(Guid eventId, bool trackChanges);
}