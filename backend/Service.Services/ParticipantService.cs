using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Service.Contracts;
using Shared.DTO.Participants;

namespace Service.Services;

internal sealed class ParticipantService : IParticipantService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ParticipantService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ParticipantDto>> GetAllParticipantsAsync(Guid eventId, bool trackChanges)
    {
        var participantsEvent = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (participantsEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participants = await _repository.Participant.GetAllParticipantsAsync(eventId, trackChanges);
        var participantsDto = _mapper.Map<IEnumerable<ParticipantDto>>(participants);

        return participantsDto;
    }

    public async Task<ParticipantDto> GetParticipantByIdAsync(Guid eventId, Guid participantId, bool trackChanges)
    {
        var participantEvent = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await _repository.Participant.GetParticipantByIdAsync(eventId, participantId, trackChanges);
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        var participantDto = _mapper.Map<ParticipantDto>(participant);
        return participantDto;
    }

    public async Task<RegistrationResult> CreateParticipantAsync(Guid eventId, ParticipantForCreationDto participantForCreation, bool trackChanges)
    {
        var participantEvent = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);

        if (participantEvent.Participants.Count >= participantEvent.MaxParticipants)
        {
            return new RegistrationResult()
            {
                IsSuccessful = false,
                Participant = null
            };
        }
        
        var participantEntity = _mapper.Map<Participant>(participantForCreation);
        
        _repository.Participant.CreateParticipant(eventId, participantEntity);
        await _repository.SaveAsync();
        
        var participantToReturn = _mapper.Map<ParticipantDto>(participantEntity);

        return new RegistrationResult()
        {
            IsSuccessful = true,
            Participant = participantToReturn
        };
    }

    public async Task DeleteParticipantAsync(Guid eventId, Guid participantId, bool trackChanges)
    {
        var participantEvent  = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await _repository.Participant.GetParticipantByIdAsync(eventId, participantId, trackChanges);
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        _repository.Participant.DeleteParticipant(participant);
        await _repository.SaveAsync();
    }
}