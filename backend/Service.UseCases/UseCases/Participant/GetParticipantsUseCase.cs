using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Participant;
using Shared.DTO.Participants;
using Shared.RequestFeatures;

namespace Service.UseCases.UseCases.Participant;

public class GetParticipantsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetParticipantsUseCase
{
    public async Task<(IEnumerable<ParticipantDto> participants, MetaData metaData)> ExecuteAsync(Guid eventId, ParticipantParameters participantParameters, bool trackChanges)
    {
        var participantsEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (participantsEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participantsWithMetaData = await repository.Participant
            .GetParticipantsAsync(eventId, participantParameters, trackChanges);
        
        var participantsDto = mapper.Map<IEnumerable<ParticipantDto>>(participantsWithMetaData);

        return (
            participants: participantsDto, 
            metaData: participantsWithMetaData.MetaData);
    }
}