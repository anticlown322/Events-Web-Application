namespace Shared.DTO.Participants;

public class ParticipantForCreationDto
{
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string DateOfBirth { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}