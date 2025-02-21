using Application.DTO.Participants;

namespace Application.Contracts.UseCaseContracts.Participant;

public interface ICreateParticipantUseCase
{
    Task<RegistrationResult> ExecuteAsync(Guid eventId, ParticipantForCreationDto participantForCreation, 
        bool trackChanges, CancellationToken cancellationToken);
}