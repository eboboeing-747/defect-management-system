using Microsoft.AspNetCore.Http;

namespace DmsService.Object;

public record EstateObjectObject
{
    public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    public string Address { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public record EstateObjectCard
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public record EstateObjectReturn
{
    public List<string> Images { get; set; } = [];
    public string Address { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
