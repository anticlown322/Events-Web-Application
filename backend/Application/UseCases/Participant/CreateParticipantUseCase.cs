using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.Participant;
using Application.DTO.Participants;
using Application.Validation.Exceptions.Specific;
using AutoMapper;

namespace Application.UseCases.Participant;

public class CreateParticipantUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : ICreateParticipantUseCase
{
    public async Task<RegistrationResult> ExecuteAsync(Guid eventId, 
        ParticipantForCreationDto participantForCreation, bool trackChanges, CancellationToken cancellationToken)
    {
        var participantEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);

        if (participantEvent.Participants.Count >= participantEvent.MaxParticipants)
        {
            return new RegistrationResult
            {
                IsSuccessful = false,
                Participant = null
            };
        }
        
        var participantEntity = mapper.Map<Domain.Models.Participant>(participantForCreation);
        
        repository.Participant.CreateParticipant(eventId, participantEntity);
        await repository.SaveAsync();
        
        var participantToReturn = mapper.Map<ParticipantDto>(participantEntity);

        return new RegistrationResult
        {
            IsSuccessful = true,
            Participant = participantToReturn
        };
    }
}