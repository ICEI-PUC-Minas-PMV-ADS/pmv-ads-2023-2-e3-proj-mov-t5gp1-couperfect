using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Domain.Entities;
using CouperfectServer.Domain.Services;
using CouperfectServer.Infrastructure.CouperfectDatabase.Mappings;
using CouperfectServer.Infrastructure.CouperfectDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Transactions;

namespace CouperfectServer.Infrastructure.CouperfectDatabase;

public class CouperfectDbContext : DbContext, ICouperfectDbUnitOfWork
{
    private static readonly TransactionOptions _defaultTransactionOptions = new() { IsolationLevel = IsolationLevel.ReadCommitted };
    private readonly IDateTimeService dateTimeService;

    public CouperfectDbContext(DbContextOptions options, IDateTimeService dateTimeService) : base(options)
    {
        PlayerRepository = new PlayerRepository(this);
        this.dateTimeService = dateTimeService;
    }

    public IPlayerRepository PlayerRepository { get; init; }

    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<GameRoom> Rooms { get; set; }
    public virtual DbSet<GameInvite> Invites { get; set; }


    public CommittableTransaction BeginTransaction()
    {
        var transaction = new CommittableTransaction(_defaultTransactionOptions);
        Database.EnlistTransaction(transaction);
        return transaction;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: AuditableEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entry in auditEntries)
        {
            var auditableEntity = (AuditableEntity)entry.Entity;

            auditableEntity.ModifiedAt = dateTimeService.Now;


            if (entry.State is EntityState.Added)
                auditableEntity.CreatedAt = dateTimeService.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerMapping());
        modelBuilder.ApplyConfiguration(new GameRoomMapping());
        modelBuilder.ApplyConfiguration(new GameInviteMapping());

        modelBuilder.Entity<GameRoom>().HasMany(x => x.Invites).WithOne(x => x.Room);
        modelBuilder.Entity<Player>().HasMany(x => x.PlayedRooms).WithMany(x => x.Players);
        modelBuilder.Entity<GameRoom>().HasOne(x => x.Admin).WithOne(x => x.OwnedRoom).HasForeignKey<GameRoom>(x => x.AdminId);
        modelBuilder.Entity<GameRoom>().HasMany(x => x.BannedPlayers).WithMany(x => x.BannedRooms);

        // TODO: Investigar
        modelBuilder.Entity<Player>().Ignore(x => x.CurrentRoom);

        base.OnModelCreating(modelBuilder);
    }
}
