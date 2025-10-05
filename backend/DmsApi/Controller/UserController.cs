using DmsDb.Entity;
using Microsoft.Extensions.Primitives;
using DmsDb.Service;
using DmsDb.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Specialized;

namespace DmsApi.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        this._userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IResult> Login(
        [FromForm] UserCredentials user
    ) {
        (string? token, UserReturn? userToReturn) = await this._userService.Login(user.Login, user.Password);

        if (token == null)
            return Results.Unauthorized();

        var context = HttpContext;
        AddJwtToken(token, context);
        return Results.Ok(userToReturn);
    }

    [HttpPost("Register")]
    public async Task<IResult> Register(
        [FromBody] UserRegister user
    ) {
        Console.WriteLine(user);

        return await this._userService.Register(user);
    }

    [HttpGet("Restricted")]
    [Authorize(Roles = "engineer")]
    public async Task<IActionResult> DoRestricedAction()
    {
        return Ok();
    }

    private static void AddJwtToken(string token, HttpContext context)
    {
        context.Response.Cookies.Append("JwtToken", token, new CookieOptions
        {
            IsEssential = true,
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
    }
}
