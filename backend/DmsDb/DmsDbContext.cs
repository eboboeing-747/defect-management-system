using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb;

public class DmsDbContext : DbContext
{
    public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options) { }

    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<DefectEntity> Defects { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<ObjectEntity> Objects { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<DefectCommentEntity> DefectCommentEntities { get; set; }
}
