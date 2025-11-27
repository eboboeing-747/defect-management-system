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
            .HasMany(d => d.DefectComments)
            .WithMany(dc => dc.Defects)
            .UsingEntity<DefectCommentEntityDefectEntity>(
                "DefectCommentEntityDefectEntity",
                r => r.HasOne<DefectCommentEntity>(jt => jt.DefectComment)
                    .WithMany(dc => dc.DefectToDefectComments),
                l => l.HasOne<DefectEntity>(jt => jt.Defect)
                    .WithMany(d => d.DefectToDefectComments)
            );

        builder
            .HasOne(d => d.OriginalPoster)
            .WithMany(op => op.Defects)
            .HasForeignKey(d => d.OriginalPosterId)
            .IsRequired();

        builder
            .HasMany(d => d.Images)
            .WithOne(di => di.Defect)
            .HasForeignKey(di => di.DefectId)
            .IsRequired();
    }
}
