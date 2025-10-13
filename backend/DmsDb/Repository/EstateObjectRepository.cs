using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;
using DmsDb.Object;

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

    public async Task<List<EstateObjectCard>> GetAll(List<Guid> estateObjectIds)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => estateObjectIds.Contains(eo.Id))
            .Select<EstateObjectEntity, EstateObjectCard>(eo => new EstateObjectCard
            {
                Id = eo.Id,
                Name = eo.Name,
                Address = eo.Address
            })
            .ToListAsync();
    }
}
