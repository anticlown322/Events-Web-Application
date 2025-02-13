using Application.DTO.Events;

namespace Application.Contracts.UseCases.Event;

public interface IUpdateEventUseCase
{
    Task ExecuteAsync(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges);
}