using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Contracts.UseCases.Authentication;
using Shared.DTO.User;

namespace Presentation.Core.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(
    IRefreshTokenForAuthUseCase refreshTokenForAuthUseCase) 
    : ControllerBase
{
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await refreshTokenForAuthUseCase
            .ExecuteAsync(tokenDto);

        return Ok(tokenDtoToReturn);
    }
}