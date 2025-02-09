using Domain.Entities.Models;
using Shared.DTO.Participants;

namespace Service.Contracts;

public interface IParticipantService
{
    IEnumerable<ParticipantDto> GetAllParticipants(Guid eventId, bool trackChanges);
    ParticipantDto GetParticipant(Guid eventId, Guid participantId, bool trackChanges);
    RegistrationResult CreateParticipant(Guid eventId, ParticipantForCreationDto participantForCreation, 
        bool trackChanges);
    void DeleteParticipantForEvent(Guid eventId, Guid participantId, bool trackChanges);
}