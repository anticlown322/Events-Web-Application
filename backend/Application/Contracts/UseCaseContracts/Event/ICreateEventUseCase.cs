using Application.DTO.Events;

namespace Application.Contracts.UseCaseContracts.Event;

public interface ICreateEventUseCase
{
    Task<EventDto> ExecuteAsync(EventForCreationDto eventToCreate, CancellationToken cancellationToken);
}