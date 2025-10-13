using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb.Repository;

public class ImageRepository
{
    private readonly DmsDbContext _dbContext;

    public ImageRepository(
        DmsDbContext dbContext
    ) {
        this._dbContext = dbContext;
    }

    public async Task Create(string filename, Guid entityId)
    {
        ImageEntity image = new ImageEntity
        {
            Id = Guid.NewGuid(),
            EntityId = entityId,
            Path = filename
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<string?> GetThumbnail(Guid entityId)
    {
        return await _dbContext.Images
            .AsNoTracking()
            .Where(image => image.EntityId == entityId)
            .Select(image => image.Path)
            .FirstOrDefaultAsync();
    }

    public async Task<List<string>> GetFilesByEntityId(Guid entityId)
    {
        return await _dbContext.Images
            .AsNoTracking()
            .Where(image => image.EntityId == entityId)
            .Select(image => image.Path)
            .ToListAsync();
    }
}
