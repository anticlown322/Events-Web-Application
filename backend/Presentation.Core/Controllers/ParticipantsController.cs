using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Events;
using Shared.DTO.Participants;
using Shared.RequestFeatures;
using Shared.Validators;

namespace Presentation.Core.Controllers;

[Route("api/events/{eventId}/participants")]

[ApiController]
public class ParticipantsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ParticipantsController(IServiceManager service) => _service = service;
    
    [HttpGet]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> GetParticipantsForEvent(Guid eventId, 
        [FromQuery] ParticipantParameters participantParameters)
    {
        var pagedResult = await _service.ParticipantService
            .GetParticipantsAsync(eventId, participantParameters, trackChanges: false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.participants);
    }

    [HttpGet("{id:guid}", Name = "GetParticipantForEvent")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> GetParticipantForEvent(Guid eventId, Guid id)
    {
        var participant = await _service.ParticipantService.GetParticipantByIdAsync(eventId, id, trackChanges: false);
        return Ok(participant);
    }
    
    [HttpPost]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<ParticipantForCreationDto>]
    public async Task<IActionResult> CreateParticipantForEvent(Guid eventId, [FromBody] ParticipantForCreationDto participant)
    {
        if (participant is null)
            return BadRequest("ParticipantForCreationDto object is null");
        
        var registrationResult = await _service.ParticipantService
            .CreateParticipantAsync(eventId, participant, trackChanges: false);

        return Ok(registrationResult);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> DeleteParticipantForEvent(Guid eventId, Guid id)
    {
        await _service.ParticipantService.DeleteParticipantAsync(eventId, id, trackChanges: false);
        return NoContent();
    }

}