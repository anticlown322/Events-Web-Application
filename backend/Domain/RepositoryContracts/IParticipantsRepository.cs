using Domain.Models;
using Domain.RequestFeatures;

namespace Domain.RepositoryContracts;

public interface IParticipantsRepository
{
    Task<PagedList<Participant>> GetParticipantsAsync(Guid eventId, ParticipantParameters participantParameters, 
        bool trackChanges, CancellationToken cancellationToken);
    Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, 
        bool trackChanges, CancellationToken cancellationToken);
    void CreateParticipant(Guid eventId, Participant  participant);
    void DeleteParticipant(Participant participant);
    public Task<bool> IsUniqueEmailAsync(string email, CancellationToken cancellationToken);
}