namespace DmsDb.Entity;

public class DefectEntity
{
    public Guid Id { get; set; }
    public Guid OriginalPosterId { get; set; }
    public Guid EstateObjectId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<DefectCommentEntity> DefectComments { get; } = [];
    public List<DefectCommentEntityDefectEntity> DefectToDefectComments { get; } = [];

    public UserEntity OriginalPoster { get; set; } = null!;
    public EstateObjectEntity EstateObject { get; set; } = null!;
    public List<DefectImageEntity> Images { get; set; } = [];
}
