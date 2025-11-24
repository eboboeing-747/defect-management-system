using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasMany(u => u.EstateObjects)
            .WithMany(eo => eo.Users)
            .UsingEntity<UserEstateObjectEntity>();

        builder
            .HasMany(u => u.Defects)
            .WithOne(d => d.OriginalPoster)
            .HasForeignKey(d => d.OriginalPosterId)
            .IsRequired(false);
    }
}
