using CouperfectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouperfectServer.Infrastructure.CouperfectDatabase.Mappings;

public class PlayerMapping : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.PasswordSalt);
        builder.Property(x => x.PasswordHash);
        builder.Property(x => x.Email);
    }
}
