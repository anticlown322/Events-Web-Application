using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.Core.ModelBinders;
using Service.Contracts;
using Shared.DTO.Events;

namespace Presentation.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IServiceManager service, ILoggerManager logger) : ControllerBase
{
    private readonly IServiceManager _service = service;
    private ILoggerManager _logger = logger;

    [HttpGet]
    public IActionResult GetEvents()
    {
        var events = _service.EventService.GetAllEvents(trackChanges: false);
        return Ok(events);
    }
    
    [HttpGet("{id:guid}", Name = "EventById")]
    public IActionResult GetEvent(Guid id)
    {
        var eventToGet = _service.EventService.GetEvent(id, trackChanges: false);
        return Ok(eventToGet);
    }
    
    [HttpGet("collection/({ids})", Name = "EventCollection")]
    public IActionResult GetEventCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var events = _service.EventService.GetEventsByIds(ids, trackChanges: false);
        return Ok(events);
    }
    
    [HttpPost]
    public IActionResult CreateEvent([FromBody] EventForCreationDto eventToCreate)
    {
        if (eventToCreate is null)
            return BadRequest("EventForCreationDto object is null");
        
        var createdEvent = _service.EventService.CreateEvent(eventToCreate);
        return CreatedAtRoute("EventById", new { id = createdEvent.Id }, createdEvent);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEvent(Guid id)
    {
        _service.EventService.DeleteEvent(id, trackChanges: false);
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpdateEvent(Guid id, [FromBody] EventForUpdateDto eventForUpdate)
    {
        if (eventForUpdate is null)
            return BadRequest("EventForUpdateDto object is null");
        
        _service.EventService.UpdateEvent(id, eventForUpdate, trackChanges: true);
        return NoContent();
    }

}