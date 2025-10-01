using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmsDb.Configuration;

public class DefectConfiguration : IEntityTypeConfiguration<DefectEntity>
{
    public void Configure(EntityTypeBuilder<DefectEntity> builder)
    {
        builder.HasKey(defect => defect.Id);
    }
}
