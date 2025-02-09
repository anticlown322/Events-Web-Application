namespace Shared.DTO.Participants;

public class ParticipantDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string DateOfBirth { get; init; }
    public string Email { get; init; } = string.Empty;
    public string RegistrationTime { get; init; }
    public Guid EventId { get; init; }
}