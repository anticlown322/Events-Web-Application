using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.User;

namespace Presentation.Core.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _service;
    public TokenController(IServiceManager service) => _service = service;
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}