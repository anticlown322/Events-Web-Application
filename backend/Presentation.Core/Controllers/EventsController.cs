using System.Text.Json;
using Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Core.ModelBinders;
using Service.Contracts;
using Shared.DTO.Events;
using Shared.RequestFeatures;
using Shared.Validators;

namespace Presentation.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IServiceManager service, ILoggerManager logger) : ControllerBase
{
    private readonly IServiceManager _service = service;
    private ILoggerManager _logger = logger;

    [HttpGet]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEvents([FromQuery] EventParameters eventParameters)
    {
        var pagedResult = await _service.EventService
            .GetAllEventsAsync(eventParameters, trackChanges: false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.events);
    }
    
    [HttpGet("{id:guid}", Name = "EventById")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var eventToGet = await _service.EventService.GetEventByIdAsync(id, trackChanges: false);
        return Ok(eventToGet);
    }
    
    [HttpGet("{name}", Name = "EventByName")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var eventToGet = await _service.EventService.GetEventByNameAsync(name, trackChanges: false);
        return Ok(eventToGet);
    }
    
    [HttpGet("collection/({ids})", Name = "EventCollection")]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var events = await _service.EventService.GetEventsByIdsAsync(ids, trackChanges: false);
        return Ok(events);
    }
    
    [HttpPost]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForCreationDto>]
    public async Task<IActionResult> CreateEvent([FromBody] EventForCreationDto eventToCreate)
    {
        if (eventToCreate is null)
            return BadRequest("EventForCreationDto object is null");
        
        var createdEvent = await _service.EventService.CreateEventAsync(eventToCreate);
        return CreatedAtRoute("EventById", new { id = createdEvent.Id }, createdEvent);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _service.EventService.DeleteEventAsync(id, trackChanges: false);
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<EventForUpdateDto>]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventForUpdateDto eventForUpdate)
    {
        if (eventForUpdate is null)
            return BadRequest("EventForUpdateDto object is null");
        
        await _service.EventService.UpdateEventAsync(id, eventForUpdate, trackChanges: true);
        return NoContent();
    }
}