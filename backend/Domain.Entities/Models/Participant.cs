using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models;

public class Participant
{
    [Column("ParticipantId")]
    public Guid Id { get; init; } 
    
    [Required(ErrorMessage = "Participant name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Participant surname is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Surname is 100 characters.")]
    public string Surname { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Date of birth is a required field.")]
    public DateOnly DateOfBirth { get; init; }

    [Required(ErrorMessage = "Email is a required field.")]
    [MaxLength(320, ErrorMessage = "Maximum length for the Email is 320 characters.")]
    public string Email { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Registration time is a required field.")]
    public DateTime RegistrationTime { get; set; }
    
    [ForeignKey(nameof(Event))]
    public Guid EventId { get; set; }
    
    public Event? Event { get; set; }
}