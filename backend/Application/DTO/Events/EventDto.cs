using Domain.Models;

namespace Application.DTO.Events;

public class EventDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public Category Category { get; init; }
    public int MaxParticipants { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
}