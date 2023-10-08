using System.Transactions;

namespace CouperfectServer.Application.Contracts.CouperfectDb;

public interface ICouperfectDbUnitOfWork
{
    IPlayerRepository PlayerRepository { get; }

    CommittableTransaction BeginTransaction();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
