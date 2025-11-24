using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb;

public class DmsDbContext : DbContext
{
    public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EstateObjectEntity> EstateObjects { get; set; }
    public DbSet<EstateObjectImageEntity> EstateObjectImages { get; set; }
    public DbSet<DefectEntity> Defects { get; set; }
    public DbSet<DefectImageEntity> DefectImages { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<UserEstateObjectEntity> UserEstateObjects { get; set; }
}
