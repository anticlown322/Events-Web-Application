using Domain.Contracts;
using Domain.Entities.Models;

namespace Domain.Repository.Repositories;

public class ParticipantsRepository : RepositoryBase<Participant>, IParticipantsRepository
{
    public ParticipantsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {}
    
    public IEnumerable<Participant> GetAllParticipants(Guid eventId, bool trackChanges) =>
    FindByCondition(p => p.EventId.Equals(eventId), trackChanges)
        .OrderBy(p => p.Name).ToList();
    
    public Participant GetParticipant(Guid eventId, Guid participantId, bool trackChanges) =>
        FindByCondition(p => p.EventId.Equals(eventId) && p.Id.Equals(participantId), trackChanges)
            .SingleOrDefault();

    public void CreateParticipantForEvent(Guid eventId, Participant participant)
    {
        participant.EventId = eventId;
        participant.RegistrationTime = DateTime.UtcNow;
        Create(participant);
    }
    
    public void DeleteParticipant(Participant participant) => Delete(participant);
}