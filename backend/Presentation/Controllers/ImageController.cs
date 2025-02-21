using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Application.Contracts.UseCaseContracts.Image;

namespace Presentation.Controllers;

[Route("api/events/{eventId}/image")]
[ApiController]
public class ImageController(
    IGetImageUseCase getImageUseCase) 
    : ControllerBase
{
    [HttpGet]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventImage(Guid eventId, CancellationToken cancellationToken)
    {
        var image = await getImageUseCase
            .ExecuteAsync(eventId, trackChanges: false, cancellationToken);
        
        return File(
            image.fileBytes, 
            image.contentType, 
            image.filename);
    }
}