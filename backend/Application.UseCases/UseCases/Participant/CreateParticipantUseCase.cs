using AutoMapper;
using Domain.Contracts;
using Application.Contracts.UseCases.Participant;
using Application.DTO.Participants;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Participant;

public class CreateParticipantUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : ICreateParticipantUseCase
{
    public async Task<RegistrationResult> ExecuteAsync(Guid eventId, ParticipantForCreationDto participantForCreation, bool trackChanges)
    {
        var participantEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
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
        
        var participantEntity = mapper.Map<Domain.Entities.Models.Participant>(participantForCreation);
        
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