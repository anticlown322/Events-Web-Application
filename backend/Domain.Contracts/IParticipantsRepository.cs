using Domain.Entities.Models;

namespace Domain.Contracts;

public interface IParticipantsRepository
{
    IEnumerable<Participant> GetAllParticipants(Guid eventId, bool trackChanges);
    Participant GetParticipant(Guid eventId, Guid participantId, bool trackChanges);
    void CreateParticipantForEvent(Guid eventId, Participant  participant);
    void DeleteParticipant(Participant participant);
}