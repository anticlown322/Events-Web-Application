using Domain.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.Events;

public class EventForCreationDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public Category Category { get; init; }
    public int MaxParticipants { get; init; }
    public IFormFile? Image { get; set; }
}