using Domain.Contracts;
using Domain.Repository.Repositories;

namespace Domain.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IEventsRepository> _eventsRepository;
    private readonly Lazy<IParticipantsRepository> _participantsRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        
        _eventsRepository = new Lazy<IEventsRepository>(() => new
            EventsRepository(repositoryContext));
        
        _participantsRepository = new Lazy<IParticipantsRepository>(() => new
            ParticipantsRepository(repositoryContext));
    }
    
    public IEventsRepository Event => _eventsRepository.Value;
    public IParticipantsRepository Participant => _participantsRepository.Value;
    
    public void Save() => _repositoryContext.SaveChanges();
}
