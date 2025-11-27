using DmsDb.Entity;
using DmsDb.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DmsDb;

public class DmsDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration<UserEntity>(new UserConfiguration())
            .ApplyConfiguration<EstateObjectEntity>(new EstateObjectConfiguration())
            .ApplyConfiguration<DefectEntity>(new DefectConfiguration());
    }

    public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EstateObjectEntity> EstateObjects { get; set; }
    public DbSet<EstateObjectImageEntity> EstateObjectImages { get; set; }
    public DbSet<DefectEntity> Defects { get; set; }
    public DbSet<DefectImageEntity> DefectImages { get; set; }
    public DbSet<DefectCommentEntity> DefectComments { get; set; }
}
