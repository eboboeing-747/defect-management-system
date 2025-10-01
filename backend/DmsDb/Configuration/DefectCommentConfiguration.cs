using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class DefectCommentConfiguration : IEntityTypeConfiguration<DefectCommentEntity>
{
    public void Configure(EntityTypeBuilder<DefectCommentEntity> builder)
    {
        builder.HasKey(defectComment => defectComment.Id);
    }
}
