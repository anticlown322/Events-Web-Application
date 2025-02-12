using Shared.DTO.Participants;

namespace Service.Contracts.UseCases.Participant;

public interface ICreateParticipantUseCase
{
    Task<RegistrationResult> ExecuteAsync(
        Guid eventId, ParticipantForCreationDto participantForCreation, bool trackChanges);
}