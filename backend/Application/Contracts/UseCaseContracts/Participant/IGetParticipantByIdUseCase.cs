using Application.DTO.Participants;

namespace Application.Contracts.UseCaseContracts.Participant;

public interface IGetParticipantByIdUseCase
{
    Task<ParticipantDto> ExecuteAsync(
        Guid eventId, Guid participantId, bool trackChanges, CancellationToken cancellationToken);
}