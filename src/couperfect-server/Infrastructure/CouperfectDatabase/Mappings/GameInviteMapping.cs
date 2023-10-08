using CouperfectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouperfectServer.Infrastructure.CouperfectDatabase.Mappings;

public class GameInviteMapping : IEntityTypeConfiguration<GameInvite>
{
    public void Configure(EntityTypeBuilder<GameInvite> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.WasUsed);
    }
}
