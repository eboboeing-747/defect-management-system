using DmsDb.Entity;

namespace DmsDb.Repository;

public class ImageRepository
{
    private readonly DmsDbContext _dbContext;

    public ImageRepository(
        DmsDbContext dbContext
    ) {
        this._dbContext = dbContext;
    }

    public async Task Create(string filename, Guid entityId)
    {
        ImageEntity image = new ImageEntity
        {
            Id = Guid.NewGuid(),
            EntityId = entityId,
            Path = filename
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();
    }
}
