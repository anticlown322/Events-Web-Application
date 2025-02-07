using Domain.Contracts;
using Domain.Entities.Models;

namespace Domain.Repository.Repositories;

public class ParticipantsRepository : RepositoryBase<Participant>, IParticipantsRepository
{
    public ParticipantsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {}
}