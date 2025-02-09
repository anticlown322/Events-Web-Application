using Domain.Entities.Models;
using Shared.DTO.Participants;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IParticipantService
{
    Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> GetParticipantsAsync(Guid eventId, 
        ParticipantParameters participantParameters, bool trackChanges);
    Task<ParticipantDto> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges);
    Task<RegistrationResult> CreateParticipantAsync(Guid eventId, ParticipantForCreationDto participantForCreation, 
        bool trackChanges);
    Task DeleteParticipantAsync(Guid eventId, Guid participantId, bool trackChanges);
}