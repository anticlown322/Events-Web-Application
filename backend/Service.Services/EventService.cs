using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Service.Contracts;
using Shared.DTO.Events;
using Shared.RequestFeatures;

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

    public async Task<(IEnumerable<EventDto> events, MetaData metaData)> GetAllEventsAsync(
        EventParameters eventParameters, bool trackChanges)
    {
        var eventsWithMetaData = await _repository.Event.GetAllEventsAsync(eventParameters, trackChanges);

        var eventsDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);

        return (
            events: eventsDto, 
            metaData: eventsWithMetaData.MetaData);
    }

    public async Task<EventDto> GetEventByIdAsync(Guid eventId, bool trackChanges)
    {
        var eventToGet = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        var eventDto = _mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }

    public async Task<EventDto> GetEventByNameAsync(string name, bool trackChanges)
    {
        var eventToGet = await _repository.Event.GetEventByNameAsync(name, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByNameException(name);

        var eventDto = _mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }

    public async Task<EventDto> CreateEventAsync(EventForCreationDto eventToCreate)
    {
        var eventEntity = _mapper.Map<Event>(eventToCreate);

        _repository.Event.CreateEvent(eventEntity);
        await _repository.SaveAsync();

        var eventToReturn = _mapper.Map<EventDto>(eventEntity);

        return eventToReturn;
    }

    public async Task<IEnumerable<EventDto>> GetEventsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var eventsEntities = await _repository.Event.GetEventsByIdsAsync(ids, trackChanges);
        if (ids.Count() != eventsEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var eventsToReturn = _mapper.Map<IEnumerable<EventDto>>(eventsEntities);
        return eventsToReturn;
    }

    public async Task DeleteEventAsync(Guid eventId, bool trackChanges)
    {
        var eventToGet = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        _repository.Event.DeleteEvent(eventToGet);
        await _repository.SaveAsync();
    }

    public async Task UpdateEventAsync(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges)
    {
        var eventEntity = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventEntity is null)
            throw new EventNotFoundByIdException(eventId);

        _mapper.Map(eventToUpdate, eventEntity);
        await _repository.SaveAsync();
    }
}