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
}
