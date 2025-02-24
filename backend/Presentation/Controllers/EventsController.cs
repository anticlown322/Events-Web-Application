using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Application.Validation;
using Domain.RequestFeatures;
using Presentation.ModelBinders;

namespace Presentation.Controllers;

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
    public async Task<IActionResult> GetEvents([FromQuery] EventParameters eventParameters, CancellationToken cancellationToken)
    {
        var pagedResult = await getEventsUseCase
            .ExecuteAsync(eventParameters, trackChanges: false, cancellationToken);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.events);
    }
    
    [HttpGet("{id:guid}", Name = "EventById")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventById(Guid id, CancellationToken cancellationToken)
    {
        var eventToGet = await getEventByIdUseCase
            .ExecuteAsync(id, trackChanges: false, cancellationToken);
        
        return Ok(eventToGet);
    }
    
    [HttpGet("{name}", Name = "EventByName")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventByName(string name, CancellationToken cancellationToken)
    {
        var eventToGet = await getEventByNameUseCase
            .ExecuteAsync(name, trackChanges: false, cancellationToken);
        
        return Ok(eventToGet);
    }
    
    [HttpGet("collection/({ids})", Name = "EventCollection")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventCollection(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var events = await getEventCollectionByIdsUseCase
            .ExecuteAsync(ids, trackChanges: false, cancellationToken);
        
        return Ok(events);
    }
    
    [HttpPost]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForCreationDto>]
    public async Task<IActionResult> CreateEvent([FromForm] EventForCreationDto eventToCreate, 
        CancellationToken cancellationToken)
    {
        var createdEvent = await createEventUseCase.ExecuteAsync(eventToCreate, cancellationToken);
        return CreatedAtRoute("EventById", new { id = createdEvent.Id }, createdEvent);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        await deleteEventUseCase.ExecuteAsync(id, trackChanges: false, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForUpdateDto>]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventForUpdateDto eventForUpdate,
        CancellationToken cancellationToken)
    {
        await updateEventUseCase.ExecuteAsync(id, eventForUpdate, trackChanges: true, cancellationToken);
        return NoContent();
    }
}