using AutoMapper;
using Domain.Contracts;
using Application.Contracts.UseCases.Participant;
using Application.DTO.Participants;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Participant;

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