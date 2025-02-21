using Domain.Models;
using Application.RequestFeatures;

namespace Application.Contracts.RepositoryContracts;

public interface IParticipantsRepository
{
    Task<PagedList<Participant>> GetParticipantsAsync(Guid eventId, ParticipantParameters participantParameters, 
        bool trackChanges, CancellationToken cancellationToken);
    Task<Participant> GetParticipantByIdAsync(Guid eventId, Guid participantId, 
        bool trackChanges, CancellationToken cancellationToken);
    void CreateParticipant(Guid eventId, Participant  participant);
    void DeleteParticipant(Participant participant);
}