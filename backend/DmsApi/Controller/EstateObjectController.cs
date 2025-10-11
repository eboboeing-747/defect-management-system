using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DmsDb.Object;
using DmsDb.Service;
using System.Net;

namespace DmsApi.Controller;

[ApiController]
[Route("[controller]")]
public class EstateObjectController : ControllerBase
{
    private readonly EstateObjectService _estateObjectService;

    public EstateObjectController(EstateObjectService estateObjectService)
    {
        this._estateObjectService = estateObjectService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        [FromForm] EstateObjectObject estateObject
    ) {
        HttpStatusCode status = await _estateObjectService.Create(estateObject);

        return Ok();
    }
}
