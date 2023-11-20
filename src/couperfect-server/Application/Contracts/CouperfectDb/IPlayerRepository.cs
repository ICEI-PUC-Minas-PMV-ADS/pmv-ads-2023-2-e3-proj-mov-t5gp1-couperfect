using CouperfectServer.Domain.Entities;

namespace CouperfectServer.Application.Contracts.CouperfectDb;

public interface IPlayerRepository
{
    Task<Player?> Find(int id, CancellationToken cancellationToken = default);
    Task<Player?> Find(string email, CancellationToken cancellationToken = default);
    Task Create(Player player, CancellationToken cancellationToken = default);
    void Update(Player player);
}
