using CouperfectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouperfectServer.Infrastructure.CouperfectDatabase.Mappings;

// TODO: Remover salas da persistencia do banco e deixar apenas em memoria
public class GameRoomMapping : IEntityTypeConfiguration<GameRoom>
{
    public void Configure(EntityTypeBuilder<GameRoom> builder)
    {
        builder.Property(x => x.IsInviteOnly);
        builder.Property(x => x.Name);
    }
}
