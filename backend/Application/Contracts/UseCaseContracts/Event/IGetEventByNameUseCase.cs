using Application.DTO.Events;

namespace Application.Contracts.UseCaseContracts.Event;

public interface IGetEventByNameUseCase
{
    Task<EventDto> ExecuteAsync(string name, bool trackChanges, CancellationToken cancellationToken);
}