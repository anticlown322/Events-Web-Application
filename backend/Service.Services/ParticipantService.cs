using Domain.Contracts;
using Service.Contracts;

namespace Service.Services;

internal sealed class ParticipantService : IParticipantService
{
    private readonly IRepositoryManager _repository;

    public ParticipantService(IRepositoryManager repository)
    {
        _repository = repository;
    }
}