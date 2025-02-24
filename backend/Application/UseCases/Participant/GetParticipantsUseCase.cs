using AutoMapper;
using Application.Contracts.UseCaseContracts.Participant;
using Application.DTO.Participants;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;
using Domain.RequestFeatures;

namespace Application.UseCases.Participant;

public class GetParticipantsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetParticipantsUseCase
{
    public async Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> ExecuteAsync(Guid eventId, 
        ParticipantParameters participantParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var participantsEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if (participantsEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participantsWithMetaData = await repository.Participant
            .GetParticipantsAsync(eventId, participantParameters, trackChanges, cancellationToken);
        
        var participantsDto = mapper.Map<IEnumerable<ParticipantDto>>(participantsWithMetaData);

        return (
            participants: participantsDto, 
            metaData: participantsWithMetaData.MetaData);
    }
}