using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.User;
using Shared.Validators;

namespace Presentation.Core.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;
    
    [HttpPost]
    [ValidationFilter<UserForRegistrationDto>]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
        
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        
        return StatusCode(201);
    }

    [HttpPost("login")]
    [ValidationFilter<UserForAuthenticationDto>]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await _service.AuthenticationService.ValidateUser(user))
            return Unauthorized();
        
        var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);
        
        return Ok(tokenDto);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}