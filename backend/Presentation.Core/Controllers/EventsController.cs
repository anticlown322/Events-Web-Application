using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IServiceManager service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCompanies()
    {
        try
        {
            var companies = service.EventService.GetAllEvents(trackChanges: false);
            return Ok(companies);
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

}
