using Shared.DTO.Events;

namespace Service.Contracts.UseCases.Event;

public interface IUpdateEventUseCase
{
    Task ExecuteAsync(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges);
}