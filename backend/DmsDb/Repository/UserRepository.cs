using DmsDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace DmsDb.Repository;

public class UserRepository
{
    private readonly DmsDbContext _dbContext;

    public UserRepository(DmsDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<bool> Exists(string login)
    {
        return await this._dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Login == login);
    }

    public async Task<bool> Exists(Guid Id)
    {
        return await this._dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Id == Id);
    }

    public async Task<UserEntity?> GetByLogin(string login)
    {
        return await this._dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<UserEntity?> GetById(Guid Id)
    {
        return await this._dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == Id);
    }

    public async Task<bool> Create(UserEntity user)
    {
        if (await this.Exists(user.Login))
            return false;

        user.Id = Guid.NewGuid();

        await this._dbContext.Users.AddAsync(user);
        await this._dbContext.SaveChangesAsync();

        return true;
    }

    // public async Task<bool> Update(Guid userId, UserUpdate userToUpdate)
    // {
    //     if (!await this.Exists(userId))
    //         return false;
    //
    //     await this._dbContext.Users
    //         .Where(user => user.Id == userId)
    //         .ExecuteUpdateAsync(setters => setters
    //                 .SetProperty(user => user.FirstName, userToUpdate.FirstName)
    //                 .SetProperty(user => user.LastName, userToUpdate.LastName)
    //                 .SetProperty(user => user.PfpPath, userToUpdate.pfpPath)
    //                 );
    //
    //     return true;
    // }

    // public async Task<List<UserEntity>> Find(string query)
    // {
    //     return await this._dbContext.Users
    //         .AsNoTracking()
    //         .Where(user => EF.Functions.ILike(
    //                     user.Login + " " + user.FirstName + " " + user.LastName, $@"%{query}%"
    //                     ))
    //         .ToListAsync();
    // }

    public bool IsEmpty()
    {
        return !this._dbContext.Users.Any(user => true);
    }
}
