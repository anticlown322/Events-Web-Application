namespace Domain.Contracts;

public interface IRepositoryManager
{
    IEventsRepository Event { get; }
    IParticipantsRepository Participant { get; }
    Task SaveAsync();
}