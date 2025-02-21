using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts.UseCaseContracts.Participant;
using Application.DTO.Participants;
using Application.RequestFeatures;
using Application.Validation;

namespace Presentation.Controllers;

[Route("api/events/{eventId}/participants")]

[ApiController]
public class ParticipantsController(
    ICreateParticipantUseCase createParticipantUseCase,
    IDeleteParticipantUseCase deleteParticipantUseCase,
    IGetParticipantByIdUseCase getParticipantByIdUseCase,
    IGetParticipantsUseCase getParticipantsUseCase)
    : ControllerBase
{
    [HttpGet]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> GetParticipantsForEvent(Guid eventId, 
        [FromQuery] ParticipantParameters participantParameters, CancellationToken cancellationToken)
    {
        var pagedResult = await getParticipantsUseCase
            .ExecuteAsync(eventId, participantParameters, trackChanges: false, cancellationToken);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.participants);
    }

    [HttpGet("{id:guid}", Name = "GetParticipantForEvent")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> GetParticipantForEvent(Guid eventId, Guid id, CancellationToken cancellationToken)
    {
        var participant = await getParticipantByIdUseCase
            .ExecuteAsync(eventId, id, trackChanges: false, cancellationToken);
        return Ok(participant);
    }
    
    [HttpPost]
    [Authorize(Policy= "AdminOnly")]
    [ValidationFilter<ParticipantForCreationDto>]
    public async Task<IActionResult> CreateParticipantForEvent(
        Guid eventId, [FromBody] ParticipantForCreationDto participant, CancellationToken cancellationToken)
    {
        var registrationResult = await createParticipantUseCase
            .ExecuteAsync(eventId, participant, trackChanges: false, cancellationToken);

        return Ok(registrationResult);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy= "AdminOnly")]
    public async Task<IActionResult> DeleteParticipantForEvent(
        Guid eventId, Guid id, CancellationToken cancellationToken)
    {
        await deleteParticipantUseCase.ExecuteAsync(eventId, id, trackChanges: false, cancellationToken);
        return NoContent();
    }

}