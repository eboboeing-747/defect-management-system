namespace DmsDb.Entity;

public class EstateObjectEntity
{
    public Guid Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<UserEntity> Users { get; } = [];
    public List<EstateObjectEntityUserEntity> UserToEstateObjects { get; } = [];

    public List<DefectEntity> Defects { get; } = [];
    public List<EstateObjectImageEntity> Images { get; } = [];
}
