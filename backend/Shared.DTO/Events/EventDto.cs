using Domain.Entities.Models;

namespace Shared.DTO.Events;

public record EventDto(
    Guid Id,
    string Name,
    string Description,
    string StartDate,
    string Location,
    Category Category,
    int MaxParticipants,
    string ImageUrl
    );