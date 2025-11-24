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
            .Include(eo => eo.Images)
            .FirstOrDefaultAsync();
    }

    public async Task<List<EstateObjectEntity>> GetByUserId(Guid userId)
    {
        return await _dbContext.EstateObjects
            .AsNoTracking()
            .Where(eo => eo.Users.Any(u => u.Id == userId))
            .Include(eo => eo.Images.FirstOrDefault())
            .ToListAsync();

        Task<List<EstateObjectEntity>> list = (
            from ueo in _dbContext.UserEstateObjects
            join eo in _dbContext.EstateObjects.Include(e => e.Images) on ueo.EsatateObjectId equals eo.Id
            where ueo.UserId == userId
            select eo
            // select new EstateObjectEntity
            // {
            //     Id = eo.Id,
            //     Address = eo.Address,
            //     Name = eo.Name,
            //     Description = eo.Description,
            //     Images = eo.Images.Select(i => new EstateObjectImageEntity
            //     {
            //         Id = i.Id,
            //         EstateObjectId = null,
            //         Name = i.Name,
            //         Path = i.Path,
            //         EstateObject = null,
            //     }).ToListAsync()
            // }
        ).ToListAsync();

        return await list;
    }
}
