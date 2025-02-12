using Shared.DTO.Events;

namespace Service.Contracts.UseCases.Event;

public interface ICreateEventUseCase
{
    Task<EventDto> ExecuteAsync(EventForCreationDto eventToCreate);
}