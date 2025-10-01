namespace DmsDb.Entity;

public class CommentEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DefectId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Contents { get; set; } = string.Empty;
}
