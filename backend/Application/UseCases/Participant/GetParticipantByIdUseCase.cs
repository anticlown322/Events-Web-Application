using AutoMapper;
using Application.Contracts.UseCaseContracts.Participant;
using Application.DTO.Participants;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;

namespace Application.UseCases.Participant;

public class GetParticipantByIdUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetParticipantByIdUseCase
{
    public async Task<ParticipantDto> ExecuteAsync(Guid eventId, Guid participantId, 
        bool trackChanges, CancellationToken cancellationToken)
    {
        var participantEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await repository.Participant
            .GetParticipantByIdAsync(eventId, participantId, trackChanges, cancellationToken);
        
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        var participantDto = mapper.Map<ParticipantDto>(participant);
        return participantDto;
    }
}