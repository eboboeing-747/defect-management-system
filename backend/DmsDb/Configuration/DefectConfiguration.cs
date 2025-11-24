using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class DefectConfiguration : IEntityTypeConfiguration<DefectEntity>
{
    public void Configure(EntityTypeBuilder<DefectEntity> builder)
    {
        builder
            .HasKey(d => d.Id);

        builder
            .HasOne(d => d.OriginalPoster)
            .WithMany(op => op.Defects)
            .HasForeignKey(d => d.OriginalPosterId)
            .IsRequired(true);

        builder
            .HasOne(d => d.EstateObject)
            .WithMany(eo => eo.Defects)
            .HasForeignKey(d => d.EstateObjectId)
            .IsRequired(true);

        builder
            .HasMany(d => d.Images)
            .WithOne(di => di.Defect)
            .HasForeignKey(di => di.DefectId)
            .IsRequired(true);
    }
}
