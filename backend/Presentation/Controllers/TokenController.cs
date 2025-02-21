using Microsoft.AspNetCore.Mvc;
using Application.Contracts.UseCaseContracts.Authentication;
using Application.DTO.User;

namespace Presentation.Controllers;

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