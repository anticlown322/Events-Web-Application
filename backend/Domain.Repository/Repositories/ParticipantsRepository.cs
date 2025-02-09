using Domain.Contracts;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository.Repositories;

public class ParticipantsRepository : RepositoryBase<Participant>, IParticipantsRepository
{
    public ParticipantsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {}
    
    public async Task<IEnumerable<Participant>> GetAllParticipantsAsync(Guid eventId, bool trackChanges) =>
    await FindByCondition(p => p.EventId.Equals(eventId), trackChanges)
        .OrderBy(p => p.Name).ToListAsync();
    
    public async Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges) =>
        await FindByCondition(p => p.EventId.Equals(eventId) && p.Id.Equals(participantId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateParticipant(Guid eventId, Participant participant)
    {
        participant.EventId = eventId;
        participant.RegistrationTime = DateTime.UtcNow;
        Create(participant);
    }
    
    public void DeleteParticipant(Participant participant) => Delete(participant);
}