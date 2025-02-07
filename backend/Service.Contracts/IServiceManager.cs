namespace Service.Contracts;

public interface IServiceManager
{
    IEventService EventService { get; }
    IParticipantService ParticipantService { get; }
}