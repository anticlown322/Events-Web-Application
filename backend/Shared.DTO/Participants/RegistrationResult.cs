namespace Shared.DTO.Participants;

public class RegistrationResult
{
    public bool IsSuccessful  { get; init; }
    public ParticipantDto? Participant { get; init; }
}