using Domain.Contracts;
using Service.Contracts;

namespace Service.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IEventService> _eventService;    
    private readonly Lazy<IParticipantService> _participantService;

    public ServiceManager(IRepositoryManager repositoryManager,  ILoggerManager logger)
    {
        _eventService = new Lazy<IEventService>(()  => new EventService(repositoryManager, logger));
        _participantService = new Lazy<IParticipantService>(() => new ParticipantService(repositoryManager));
    }
    
    public IEventService EventService => _eventService.Value;
    public IParticipantService ParticipantService => _participantService.Value;
}