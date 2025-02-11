using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IEventService> _eventService;    
    private readonly Lazy<IParticipantService> _participantService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    
    public ServiceManager(IRepositoryManager repositoryManager,  ILoggerManager logger, 
        IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _eventService = new Lazy<IEventService>(()  => new EventService(repositoryManager, mapper));
        _participantService = new Lazy<IParticipantService>(() => new ParticipantService(repositoryManager, mapper));
        _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(logger, mapper, userManager, configuration));
    }
    
    public IEventService EventService => _eventService.Value;
    public IParticipantService ParticipantService => _participantService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}