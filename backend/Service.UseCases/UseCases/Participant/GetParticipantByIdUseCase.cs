using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Participant;
using Shared.DTO.Participants;

namespace Service.UseCases.UseCases.Participant;

public class GetParticipantByIdUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetParticipantByIdUseCase
{
    public async Task<ParticipantDto> ExecuteAsync(Guid eventId, Guid participantId, bool trackChanges)
    {
        var participantEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await repository.Participant.GetParticipantByIdAsync(eventId, participantId, trackChanges);
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        var participantDto = mapper.Map<ParticipantDto>(participant);
        return participantDto;
    }
}