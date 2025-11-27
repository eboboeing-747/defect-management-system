namespace DmsDb.Entity;

public class EstateObjectEntityUserEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EstateObjectId { get; set; }

    public UserEntity User { get; set; } = null!;
    public EstateObjectEntity EstateObject { get; set; } = null!;
}
