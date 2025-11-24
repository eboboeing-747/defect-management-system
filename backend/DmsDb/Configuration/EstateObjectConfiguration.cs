using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class EstateObjectConfiguration : IEntityTypeConfiguration<EstateObjectEntity>
{
    public void Configure(EntityTypeBuilder<EstateObjectEntity> builder)
    {
        builder
            .HasKey(eoe => eoe.Id);

        builder
            .HasMany(eo => eo.Users)
            .WithMany(u => u.EstateObjects);

        builder
            .HasMany(eo => eo.Defects)
            .WithOne(d => d.EstateObject)
            .HasForeignKey(d => d.EstateObjectId)
            .IsRequired(false);

        builder
            .HasMany(eo => eo.Images)
            .WithOne(i => i.EstateObject)
            .HasForeignKey(i => i.EsateObjectId)
            .IsRequired(false);
    }
}
