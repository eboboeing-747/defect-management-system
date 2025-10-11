using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class ObjectConfiguration : IEntityTypeConfiguration<EstateObjectEntity>
{
    public void Configure(EntityTypeBuilder<EstateObjectEntity> builder)
    {
        builder.HasKey(obj => obj.Id);
    }
}
