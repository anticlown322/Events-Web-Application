using Domain.Entities.Models;

namespace Shared.RequestFeatures;

public class EventParameters : RequestParameters
{
    public DateTime? StartDate { get; set; }
    public string? Location { get; set; } 
    public Category? Category { get; set; } 
    
    public string? SearchTerm { get; set; }
}