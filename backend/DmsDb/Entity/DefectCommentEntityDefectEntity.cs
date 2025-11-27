namespace DmsDb.Entity;

public class DefectCommentEntityDefectEntity
{
    public Guid Id { get; set; }
    public Guid CommentId { get; set; }
    public Guid DefectId { get; set; }

    public DefectEntity Defect { get; set; } = null!;
    public DefectCommentEntity DefectComment { get; set; } = null!;
}

