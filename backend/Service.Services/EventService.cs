using Domain.Contracts;
using Service.Contracts;

namespace Service.Services;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;

    public EventService(IRepositoryManager repository)
    {
        _repository = repository;
    }
}