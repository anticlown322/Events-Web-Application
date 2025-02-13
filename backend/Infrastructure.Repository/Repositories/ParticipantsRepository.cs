using Domain.Contracts;
using Domain.Entities.Models;
using Infrastructure.Repository;
using Infrastructure.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository.Repositories;

public class ParticipantsRepository : RepositoryBase<Participant>, IParticipantsRepository
{
    public ParticipantsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Participant>> GetParticipantsAsync(Guid eventId,
        ParticipantParameters participantParameters, bool trackChanges)
    {
        var participants= await FindByCondition(p => p.EventId.Equals(eventId), trackChanges)
            .OrderBy(p => p.Name)
            .Skip((participantParameters.PageNumber - 1) * participantParameters.PageSize)
            .Take(participantParameters.PageSize)
            .ToListAsync();
        
        var count = await FindByCondition(p => p.EventId.Equals(eventId), trackChanges).CountAsync();
        
        return new PagedList<Participant>(participants, count,
            participantParameters.PageNumber, participantParameters.PageSize);
    }
        
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