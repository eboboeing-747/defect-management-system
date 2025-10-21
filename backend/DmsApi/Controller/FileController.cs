using DmsDb.Service;
using Microsoft.AspNetCore.Mvc;

namespace DmsApi.Controller;

[ApiController]
[Route("[controller]")]
public class ImageController: ControllerBase
{
    private readonly FileService _fileService;

    public ImageController(FileService fileService)
    {
        this._fileService = fileService;
    }

    [HttpGet("GetImage/{name}")]
    public async Task<ActionResult> GetImage(string name)
    {
        string filepath = await _fileService.GetFilePath(name);

        return base.PhysicalFile(filepath, $"image/{Path.GetExtension(name)}");
    }
}
