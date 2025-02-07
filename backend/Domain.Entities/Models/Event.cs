using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models;

public class Event
{
    [Column("EventId")]
    public Guid Id { get; init; } 
    
    [Required(ErrorMessage = "Event name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; init; } = string.Empty;
    
    [MaxLength(255, ErrorMessage = "Maximum length for the Description is 255 characters")]
    public string Description { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Start date is a required field.")]
    public DateTime StartDate { get; init; }
    
    [Required(ErrorMessage = "Location is a required field.")]
    public string Location { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Category is a required field.")]
    public Category Category { get; init; }
    
    public int MaxParticipants { get; init; }
    public ICollection<Participant> Participants { get; init; } = new List<Participant>();
    public string Image { get; init; } = string.Empty;
}