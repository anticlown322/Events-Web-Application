using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Service.Contracts;
using Shared.DTO.Events;

namespace Service.Services;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public EventService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<EventDto> GetAllEvents(bool trackChanges)
    {
        var events = _repository.Event.GetAllEvents(trackChanges);

        var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);

        return eventsDto;
    }

    public EventDto GetEventById(Guid eventId, bool trackChanges)
    {
        var eventToGet = _repository.Event.GetEventById(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        var eventDto = _mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }
    
    public EventDto GetEventByName(string name, bool trackChanges)
    {
        var eventToGet = _repository.Event.GetEventByName(name, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByNameException(name);

        var eventDto = _mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }

    public EventDto CreateEvent(EventForCreationDto eventToCreate)
    {
        var eventEntity = _mapper.Map<Event>(eventToCreate);

        _repository.Event.CreateEvent(eventEntity);
        _repository.Save();

        var eventToReturn = _mapper.Map<EventDto>(eventEntity);

        return eventToReturn;
    }

    public IEnumerable<EventDto> GetEventsByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        
        var eventsEntities = _repository.Event.GetEventsByIds(ids, trackChanges);
        if (ids.Count() != eventsEntities.Count())
            throw new CollectionByIdsBadRequestException();
        
        var eventsToReturn = _mapper.Map<IEnumerable<EventDto>>(eventsEntities);
        return eventsToReturn;
    }

    public void DeleteEvent(Guid eventId, bool trackChanges)
    {
        var eventToGet = _repository.Event.GetEventById(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        _repository.Event.DeleteEvent(eventToGet);
        _repository.Save();
    }

    public void UpdateEvent(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges)
    {
        var eventEntity = _repository.Event.GetEventById(eventId, trackChanges);
        if (eventEntity is null)
            throw new EventNotFoundByIdException(eventId);

        _mapper.Map(eventToUpdate, eventEntity);
        _repository.Save();
    }
}