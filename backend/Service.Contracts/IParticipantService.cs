using Domain.Entities.Models;
using Shared.DTO.Participants;

namespace Service.Contracts;

public interface IParticipantService
{
    Task<IEnumerable<ParticipantDto>> GetAllParticipantsAsync(Guid eventId, bool trackChanges);
    Task<ParticipantDto> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges);
    Task<RegistrationResult> CreateParticipantAsync(Guid eventId, ParticipantForCreationDto participantForCreation, 
        bool trackChanges);
    Task DeleteParticipantAsync(Guid eventId, Guid participantId, bool trackChanges);
}