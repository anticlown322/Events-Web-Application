using Application.DTO.Participants;
using Domain.RequestFeatures;

namespace Application.Contracts.UseCaseContracts.Participant;

public interface IGetParticipantsUseCase
{
    Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> ExecuteAsync(Guid eventId, 
        ParticipantParameters participantParameters, bool trackChanges, CancellationToken cancellationToken);
}