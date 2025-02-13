using Application.DTO.Events;

namespace Application.Contracts.UseCases.Event;

public interface ICreateEventUseCase
{
    Task<EventDto> ExecuteAsync(EventForCreationDto eventToCreate);
}