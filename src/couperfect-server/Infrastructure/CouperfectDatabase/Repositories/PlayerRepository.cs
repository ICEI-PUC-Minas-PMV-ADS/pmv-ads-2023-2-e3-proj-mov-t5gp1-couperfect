using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouperfectServer.Infrastructure.CouperfectDatabase.Repositories;

public class PlayerRepository : IPlayerRepository
{
    protected readonly CouperfectDbContext couperfectDbContext;

    public PlayerRepository(CouperfectDbContext couperfectDbContext)
    {
        this.couperfectDbContext = couperfectDbContext;
    }

    public async Task Create(Player player, CancellationToken cancellationToken = default)
    {
        await couperfectDbContext.AddAsync(player, cancellationToken);
    }

    public Task<Player?> Find(string email, CancellationToken cancellationToken = default)
    {
        return couperfectDbContext.Players.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }
}
