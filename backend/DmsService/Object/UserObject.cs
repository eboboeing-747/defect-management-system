namespace DmsService.Object;

public record UserCredentials
{
    public string Login { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record UserRegister : UserCredentials
{
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public bool Sex { get; init; }
}

public record UserUpdate
{
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? PfpPath { get; init; } = string.Empty;
}

public record UserReturn : UserUpdate
{
    public required Guid Id { get; init; }
    public string Login { get; init; } = string.Empty;
    public bool Sex { get; init; }
    public string Role { get; init; } = string.Empty;
}
