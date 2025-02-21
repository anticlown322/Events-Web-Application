using Application.DTO.Events;

namespace Application.Contracts.UseCaseContracts.Event;

public interface IUpdateEventUseCase
{
    Task ExecuteAsync(Guid eventId, 
        EventForUpdateDto eventToUpdate, bool trackChanges, CancellationToken cancellationToken);
}