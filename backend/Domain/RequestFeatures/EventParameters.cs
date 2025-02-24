using Domain.Models;

namespace Domain.RequestFeatures;

public class EventParameters : RequestParameters
{
    public DateTime? StartDate { get; set; }
    public string? Location { get; set; } 
    public Category? Category { get; set; } 
    
    public string? SearchTerm { get; set; }
}