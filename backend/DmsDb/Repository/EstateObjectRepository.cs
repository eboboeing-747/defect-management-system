using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb.Repository;

public class EstateObjectRepository
{
    private readonly DmsDbContext _dbContext;

    public EstateObjectRepository(DmsDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Create(EstateObjectEntity estateObject)
    {
        await _dbContext.EstateObjects.AddAsync(estateObject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<EstateObjectEntity>> GetByListOfIds(List<Guid> estateObjectIds)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => estateObjectIds.Contains(eo.Id))
            .ToListAsync();
    }

    public async Task<EstateObjectEntity?> GetById(Guid estateObjectId)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => eo.Id == estateObjectId)
            .FirstOrDefaultAsync();
    }
}
