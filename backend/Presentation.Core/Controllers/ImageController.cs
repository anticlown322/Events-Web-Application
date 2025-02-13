using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;

namespace Presentation.Core.Controllers;

[Route("api/events/{eventId}/image")]
[ApiController]
public class ImageController(IImageService imageService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy= "AdminOrParticipant")]
    public async Task<IActionResult> GetEventImage(Guid eventId)
    {
        var (fileBytes, contentType, filename) = await imageService
            .GetImageAsync(eventId, trackChanges: false);
        
        return File(fileBytes, contentType, filename);
    }
}