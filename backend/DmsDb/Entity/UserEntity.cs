namespace DmsDb.Entity;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PfpPath { get; set; } = null;
    public bool Sex { get; set; }
    public string Role { get; set; } = string.Empty;

    public List<EstateObjectEntity> EstateObjects { get; } = [];
    public List<DefectEntity> Defects { get; } = [];
}
