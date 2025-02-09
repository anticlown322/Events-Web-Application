using Domain.Entities.Models;

namespace Domain.Contracts;

public interface IParticipantsRepository
{
    Task<IEnumerable<Participant>> GetAllParticipantsAsync(Guid eventId, bool trackChanges);
    Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges);
    void CreateParticipant(Guid eventId, Participant  participant);
    void DeleteParticipant(Participant participant);
}