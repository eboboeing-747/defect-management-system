namespace DmsDb.Entity;

public class EstateObjectImageEntity
{
    public Guid Id { get; set; }
    public Guid? EstateObjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;

    public EstateObjectEntity? EstateObject { get; } = null;
}
