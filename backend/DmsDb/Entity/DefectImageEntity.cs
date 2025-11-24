namespace DmsDb.Entity;

public class DefectImageEntity
{
    public Guid Id { get; set; }
    public Guid? DefectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;

    public DefectEntity? Defect { get; set; }
}
