using Application.Contracts.RepositoryContracts;
using Application.RequestFeatures;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories;

public class ParticipantsRepository : RepositoryBase<Participant>, IParticipantsRepository
{
    public ParticipantsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Participant>> GetParticipantsAsync(Guid eventId,
        ParticipantParameters participantParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var participants = await 
            FindByConditionAsync(p => p.EventId.Equals(eventId), trackChanges, cancellationToken);

        var orderedParticipants = participants.OrderBy(p => p.Name);

        var pagedParticipants = orderedParticipants
            .Skip((participantParameters.PageNumber - 1) * participantParameters.PageSize)
            .Take(participantParameters.PageSize)
            .ToList();

        var totalCount = orderedParticipants.Count();

        return new PagedList<Participant>(pagedParticipants, totalCount,
            participantParameters.PageNumber, participantParameters.PageSize);
    }
        
    public async Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, 
        bool trackChanges, CancellationToken cancellationToken)
    {
        var participantToGet = await FindByConditionAsync(
            p => p.EventId.Equals(eventId) && p.Id.Equals(participantId), trackChanges, cancellationToken);
        return participantToGet.SingleOrDefault();
    }

    public void CreateParticipant(Guid eventId, Participant participant)
    {
        participant.EventId = eventId;
        participant.RegistrationTime = DateTime.UtcNow;
        Create(participant);
    }

    public void DeleteParticipant(Participant participant) => Delete(participant);
}