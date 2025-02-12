using Shared.DTO.Participants;

namespace Service.Contracts.UseCases.Participant;

public interface IGetParticipantByIdUseCase
{
    Task<ParticipantDto> ExecuteAsync(Guid eventId, Guid participantId, bool trackChanges);
}