using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(image => image.Id);
    }
}
