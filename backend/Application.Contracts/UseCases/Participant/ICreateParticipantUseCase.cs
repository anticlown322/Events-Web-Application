using Application.DTO.Participants;

namespace Application.Contracts.UseCases.Participant;

public interface ICreateParticipantUseCase
{
    Task<RegistrationResult> ExecuteAsync(
        Guid eventId, ParticipantForCreationDto participantForCreation, bool trackChanges);
}