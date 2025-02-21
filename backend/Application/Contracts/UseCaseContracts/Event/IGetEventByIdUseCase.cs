using Application.DTO.Events;

namespace Application.Contracts.UseCaseContracts.Event;

public interface IGetEventByIdUseCase
{
    Task<EventDto> ExecuteAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken);
}