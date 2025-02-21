namespace Domain.Models;

public class Participant
{
    public Guid Id { get; init; } 
    
    public string Name { get; init; } = string.Empty;
    
    public string Surname { get; init; } = string.Empty;
    
    public DateOnly DateOfBirth { get; init; }

    public string Email { get; init; } = string.Empty;
    
    public DateTime RegistrationTime { get; set; }
    
    public Guid EventId { get; set; }
    
    public Event? Event { get; set; }
}