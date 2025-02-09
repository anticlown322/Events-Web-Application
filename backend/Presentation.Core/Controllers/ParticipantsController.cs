using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Events;
using Shared.DTO.Participants;

namespace Presentation.Core.Controllers;

[Route("api/events/{eventId}/participants")]

[ApiController]
public class ParticipantsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ParticipantsController(IServiceManager service) => _service = service;
    
    [HttpGet]
    public IActionResult GetParticipantsForEvent(Guid eventId)
    {
        var employees = _service.ParticipantService.GetAllParticipants(eventId, trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "GetParticipantForEvent")]
    public IActionResult GetParticipantForEvent(Guid eventId, Guid id)
    {
        var participant = _service.ParticipantService.GetParticipant(eventId, id, trackChanges: false);
        return Ok(participant);
    }
    
    [HttpPost]
    public IActionResult CreateParticipantForEvent(Guid eventId, [FromBody] ParticipantForCreationDto participant)
    {
        if (participant is null)
            return BadRequest("ParticipantForCreationDto object is null");
        
        var registrationResult = _service.ParticipantService
            .CreateParticipant(eventId, participant, trackChanges: false);

        return Ok(registrationResult);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteParticipantForEvent(Guid eventId, Guid id)
    {
        _service.ParticipantService.DeleteParticipantForEvent(eventId, id, trackChanges: false);
        return NoContent();
    }

}