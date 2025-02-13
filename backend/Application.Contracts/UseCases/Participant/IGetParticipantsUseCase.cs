using Application.DTO.Participants;
using Infrastructure.RequestFeatures;

namespace Application.Contracts.UseCases.Participant;

public interface IGetParticipantsUseCase
{
    Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> ExecuteAsync(
        Guid eventId, ParticipantParameters participantParameters, bool trackChanges);
}