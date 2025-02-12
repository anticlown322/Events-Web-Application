using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Event;

namespace Service.UseCases.UseCases.Event;

public class DeleteEventUseCase(
    IRepositoryManager repository) : IDeleteEventUseCase
{
    public async Task ExecuteAsync(Guid eventId, bool trackChanges)
    {
        var eventToGet = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        repository.Event.DeleteEvent(eventToGet);
        await repository.SaveAsync();
    }
}