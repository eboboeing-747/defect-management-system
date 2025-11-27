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

    public async Task<bool> Create(Guid userId, EstateObjectEntity estateObject)
    {
        UserEntity? user = await _dbContext.Users
            .Include(u => u.EstateObjects)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        _dbContext.EstateObjects.Add(estateObject);

        user.EstateObjects.Add(estateObject);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<EstateObjectEntity?> GetById(Guid estateObjectId)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => eo.Id == estateObjectId)
            .Include(eo => eo.Images)
            .FirstOrDefaultAsync();
    }

    public async Task<List<EstateObjectEntity>> GetByUserId(Guid userId)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => eo.Users.Any(u => u.Id == userId))
            .Include(eo => eo.Images
                .OrderBy(i => i.Id)
                .Take(1))
            .ToListAsync();
    }
}
