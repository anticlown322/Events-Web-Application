using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IServiceManager service, ILoggerManager logger) : ControllerBase
{
    private readonly IServiceManager _service = service;
    private ILoggerManager _logger = logger;

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _service.EventService.GetAllEvents(trackChanges: false);
        return Ok(companies);
    }
}