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

    public async Task<bool> Create(Guid estateObjectId, List<EstateObjectImageEntity> images)
    {
        EstateObjectEntity? estateObject = await _dbContext.EstateObjects
            .Where(eo => eo.Id == estateObjectId)
            .FirstOrDefaultAsync();

        if (estateObject == null)
            return false;

        foreach (EstateObjectImageEntity image in images)
            image.EstateObjectId = estateObject.Id;

        await _dbContext.EstateObjectImages.AddRangeAsync(images);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task Create(string filename, Guid estateObjectId)
    {
        EstateObjectImageEntity image = new EstateObjectImageEntity
        {
            Id = Guid.NewGuid(),
            EstateObjectId = estateObjectId,
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
