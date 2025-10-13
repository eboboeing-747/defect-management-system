using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DmsDb.Object;
using DmsDb.Service;
using System.Net;
using System.Security.Claims;

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
    [Authorize(Policy = "RequireIdClaim")]
    public async Task<IActionResult> Create(
        [FromForm] EstateObjectObject estateObject
    ) {
        Claim userIdClaim = User.FindFirst("Id")!;
        Guid userId = Guid.Parse(userIdClaim.Value);

        // if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
        //     return BadRequest("failed to parse \"Id\" claim as Guid");

        HttpStatusCode status = await _estateObjectService.Create(userId, estateObject);

        switch (status)
        {
        case HttpStatusCode.RequestEntityTooLarge:
            return StatusCode(StatusCodes.Status413RequestEntityTooLarge);
        case HttpStatusCode.UnsupportedMediaType:
            return StatusCode(StatusCodes.Status415UnsupportedMediaType);
        }

        return Created();
    }

    [HttpGet("GetAll")]
    [Authorize(Policy = "RequireIdClaim")]
    public async Task<IActionResult> GetAll()
    {
        Claim userIdClaim = User.FindFirst("Id")!;
        Guid userId = Guid.Parse(userIdClaim.Value);

        return Ok(await _estateObjectService.GetAll(userId));
    }

    // private Guid? ExtractUserId()
    // {
    //     Claim? userIdClaim = User.FindFirst("Id");
    //
    //     if (userIdClaim == null)
    //         return null;
    //         // return Unauthorized("no \"Id\" claim in authorization token");
    //
    //     if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
    //         return null;
    //         // return BadRequest("failed to parse \"Id\" claim as Guid");
    //
    //     return userId;
    // }
}
