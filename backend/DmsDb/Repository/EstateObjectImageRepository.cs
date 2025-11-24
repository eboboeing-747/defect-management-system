using Microsoft.EntityFrameworkCore;
using DmsDb.Entity;

namespace DmsDb.Repository;

public class EstateObjectImageRepository
{
    private readonly DmsDbContext _dbContext;

    public EstateObjectImageRepository(
        DmsDbContext dbContext
    ) {
        this._dbContext = dbContext;
    }

    public async Task Create(string filename, Guid entityId)
    {
        EstateObjectImageEntity image = new EstateObjectImageEntity
        {
            Id = Guid.NewGuid(),
            EstateObjectId = entityId,
            Path = filename
        };

        await _dbContext.EstateObjectImages.AddAsync(image);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<string>> GetAllOfEstateObject(Guid estateObject)
    {
        return await _dbContext.EstateObjectImages
            .AsNoTracking()
            .Where(image => image.EstateObjectId == estateObject)
            .Select(image => image.Path)
            .ToListAsync();
    }

    public async Task<string?> GetFirstOfEntity(Guid entityId)
    {
        return await _dbContext.EstateObjectImages
            .AsNoTracking()
            .Where(image => image.EstateObjectId == entityId)
            .Select(image => image.Path)
            .FirstOrDefaultAsync();
    }
}
