using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class EstateObjectConfiguration : IEntityTypeConfiguration<EstateObjectEntity>
{
    public void Configure(EntityTypeBuilder<EstateObjectEntity> builder)
    {
        builder
            .HasKey(eo => eo.Id);

        builder
            .HasMany(eo => eo.Defects)
            .WithOne(d => d.EstateObject)
            .HasForeignKey(d => d.EstateObjectId)
            .IsRequired();

        builder
            .HasMany(eo => eo.Images)
            .WithOne(eoi => eoi.EstateObject)
            .HasForeignKey(oei => oei.EstateObjectId)
            .IsRequired();
    }
}
