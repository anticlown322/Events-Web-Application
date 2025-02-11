using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Models;

public class User : IdentityUser
{
    [Required(ErrorMessage = "User first name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the first name is 100 characters.")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "User last name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the last name is 100 characters.")]
    public string LastName { get; set; } = string.Empty;
    
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}