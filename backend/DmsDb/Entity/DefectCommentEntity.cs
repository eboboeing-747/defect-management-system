namespace DmsDb.Entity;

public class DefectCommentEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DefectId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Contents { get; set; } = string.Empty;

    public List<DefectEntity> Defects { get; } = [];
    public List<DefectCommentEntityDefectEntity> DefectToDefectComments { get; } = [];
}
