using System.ComponentModel.DataAnnotations;

namespace Application.DTO.User;

public record UserForAuthenticationDto
{
    public string? UserName { get; init; }
  
    public string? Password { get; init; }
}
