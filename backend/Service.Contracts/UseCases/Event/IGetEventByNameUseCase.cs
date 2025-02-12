using Shared.DTO.Events;

namespace Service.Contracts.UseCases.Event;

public interface IGetEventByNameUseCase
{
    Task<EventDto> ExecuteAsync(string name, bool trackChanges);
}