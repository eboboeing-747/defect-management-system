using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class ObjectConfiguration : IEntityTypeConfiguration<ObjectEntity>
{
    public void Configure(EntityTypeBuilder<ObjectEntity> builder)
    {
        builder.HasKey(obj => obj.Id);
    }
}
