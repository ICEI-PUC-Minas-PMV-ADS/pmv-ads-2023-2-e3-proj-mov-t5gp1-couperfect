using CouperfectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouperfectServer.Infrastructure.CouperfectDatabase.Mappings;

public class GameRoomMapping : IEntityTypeConfiguration<GameRoom>
{
    public void Configure(EntityTypeBuilder<GameRoom> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsInviteOnly);
        builder.Property(x => x.Name);
    }
}
