using DmsService.Service;
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
    public ActionResult GetImage(string name)
    {
        string filepath = _fileService.GetFullPath(name);

        return base.PhysicalFile(filepath, $"image/{Path.GetExtension(name)}");
    }
}
