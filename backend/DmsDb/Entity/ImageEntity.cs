namespace DmsDb.Entity;

public class ImageEntity
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Desctiption { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}
