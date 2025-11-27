using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<DefectCommentEntity>
{
    public void Configure(EntityTypeBuilder<DefectCommentEntity> builder)
    {
        builder.HasKey(comment => comment.Id);
    }
}
