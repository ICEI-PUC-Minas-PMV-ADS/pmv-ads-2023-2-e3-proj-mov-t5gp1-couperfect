using CouperfectServer.Domain.Entities;

namespace CouperfectServer.Application.Contracts.CouperfectDb;

public interface IPlayerRepository
{
    Task<Player?> Find(string email, CancellationToken cancellationToken = default);
    Task Create(Player player, CancellationToken cancellationToken = default);
}
