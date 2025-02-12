using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Contracts.UseCases.Authentication;
using Shared.DTO.User;
using Shared.Validators;

namespace Presentation.Core.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController(
    IRegisterUserUseCase registerUserUseCase,
    IValidateUserUseCase validateUserUseCase,
    ICreateTokenForAuthUseCase createTokenForAuthUseCase,
    IRefreshTokenForAuthUseCase refreshTokenForAuthUseCase)
    : ControllerBase
{
    [HttpPost]
    [ValidationFilter<UserForRegistrationDto>]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await registerUserUseCase.ExecuteAsync(userForRegistration);
        
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
        if (!await validateUserUseCase.ExecuteAsync(user))
            return Unauthorized();
        
        var tokenDto = await createTokenForAuthUseCase.ExecuteAsync(user, populateExp: true);
        
        return Ok(tokenDto);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await refreshTokenForAuthUseCase.ExecuteAsync(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}