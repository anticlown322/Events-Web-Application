using System.Text.Json;
using Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Core.ModelBinders;
using Application.Contracts;
using Application.Contracts.UseCases.Event;
using Application.DTO.Events;
using Infrastructure.RequestFeatures;
using Application.Validators;

namespace Presentation.Core.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController(
    IGetEventsUseCase getEventsUseCase,
    IGetEventByIdUseCase getEventByIdUseCase,
    IGetEventByNameUseCase getEventByNameUseCase,
    IGetEventCollectionByIdsUseCase getEventCollectionByIdsUseCase,
    ICreateEventUseCase createEventUseCase,
    IDeleteEventUseCase deleteEventUseCase,
    IUpdateEventUseCase updateEventUseCase)
    : ControllerBase
{
    [HttpGet]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEvents([FromQuery] EventParameters eventParameters)
    {
        var pagedResult = await getEventsUseCase
            .ExecuteAsync(eventParameters, trackChanges: false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.events);
    }
    
    [HttpGet("{id:guid}", Name = "EventById")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var eventToGet = await getEventByIdUseCase
            .ExecuteAsync(id, trackChanges: false);
        
        return Ok(eventToGet);
    }
    
    [HttpGet("{name}", Name = "EventByName")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var eventToGet = await getEventByNameUseCase
            .ExecuteAsync(name, trackChanges: false);
        
        return Ok(eventToGet);
    }
    
    [HttpGet("collection/({ids})", Name = "EventCollection")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventCollection(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var events = await getEventCollectionByIdsUseCase
            .ExecuteAsync(ids, trackChanges: false);
        
        return Ok(events);
    }
    
    [HttpPost]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForCreationDto>]
    public async Task<IActionResult> CreateEvent([FromForm] EventForCreationDto eventToCreate)
    {
        var createdEvent = await createEventUseCase.ExecuteAsync(eventToCreate);
        return CreatedAtRoute("EventById", new { id = createdEvent.Id }, createdEvent);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await deleteEventUseCase.ExecuteAsync(id, trackChanges: false);
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForUpdateDto>]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventForUpdateDto eventForUpdate)
    {
        await updateEventUseCase.ExecuteAsync(id, eventForUpdate, trackChanges: true);
        return NoContent();
    }
}