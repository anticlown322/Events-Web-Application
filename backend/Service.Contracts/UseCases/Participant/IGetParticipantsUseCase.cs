using Shared.DTO.Participants;
using Shared.RequestFeatures;

namespace Service.Contracts.UseCases.Participant;

public interface IGetParticipantsUseCase
{
    Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> ExecuteAsync(
        Guid eventId, ParticipantParameters participantParameters, bool trackChanges);
}