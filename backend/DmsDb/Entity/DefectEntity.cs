namespace DmsDb.Entity;

public class DefectEntity
{
    public Guid Id { get; set; }
    public Guid OriginalPosterId { get; set; }
    public Guid ObjectId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
