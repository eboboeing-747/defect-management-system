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
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Login == login);
    }

    public async Task<bool> Exists(Guid Id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Id == Id);
    }

    public async Task<UserEntity?> GetByLogin(string login)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<UserEntity?> GetById(Guid Id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == Id);
    }

    public async Task<bool> Create(UserEntity user)
    {
        if (await Exists(user.Login))
            return false;

        user.Id = Guid.NewGuid();

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public bool IsEmpty()
    {
        return !_dbContext.Users.Any(user => true);
    }
}
