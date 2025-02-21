using Application.Contracts.RepositoryContracts;

namespace Application.Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    IEventsRepository Event { get; }
    IParticipantsRepository Participant { get; }
    Task SaveAsync();
}