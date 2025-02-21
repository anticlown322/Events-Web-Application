namespace Domain.Models;

public class Event
{
    public Guid Id { get; init; } 

    public string Name { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
    
    public DateTime StartDate { get; init; }
    
    public string Location { get; init; } = string.Empty;
    
    public Category Category { get; init; }
    
    public int MaxParticipants { get; init; }

    public ICollection<Participant> Participants { get; init; } = new List<Participant>();
    
    public string? Image { get; set; } = string.Empty;
}