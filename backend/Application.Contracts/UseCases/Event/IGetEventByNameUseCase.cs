using Application.DTO.Events;

namespace Application.Contracts.UseCases.Event;

public interface IGetEventByNameUseCase
{
    Task<EventDto> ExecuteAsync(string name, bool trackChanges);
}