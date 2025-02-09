using Domain.Entities.Models;
using Shared.RequestFeatures;

namespace Domain.Contracts;

public interface IParticipantsRepository
{
    Task<PagedList<Participant>> GetParticipantsAsync(Guid eventId, 
        ParticipantParameters participantParameters, bool trackChanges);
    Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges);
    void CreateParticipant(Guid eventId, Participant  participant);
    void DeleteParticipant(Participant participant);
}