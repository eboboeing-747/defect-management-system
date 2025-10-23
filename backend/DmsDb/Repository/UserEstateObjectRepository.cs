using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb.Repository;

public class UserEstateObjectRepository
{
    private readonly DmsDbContext _dbContext;

    public UserEstateObjectRepository(DmsDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Add(Guid userId, Guid estateObjectId)
    {
        UserEstateObjectEntity relation = new UserEstateObjectEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            EstateObjectId = estateObjectId
        };

        await _dbContext.UserEstateObjectEntities.AddAsync(relation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Guid>> GetAllWithUser(Guid userId)
    {
        return await _dbContext.UserEstateObjectEntities
            .AsNoTracking()
            .Where(ueo => ueo.UserId == userId)
            .Select(ueo => ueo.EstateObjectId)
            .ToListAsync();
    }
}
