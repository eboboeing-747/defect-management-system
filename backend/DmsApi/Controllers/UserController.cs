using DmsDb.Entity;
using DmsDb.Service;
using DmsDb.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DmsApi.Authorization;

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

    [HttpGet("Test")]
    public IResult Test()
    {
        return Results.Ok("ok");
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

    [HttpGet("Restricted")]
    [AuthorizeRole("engineer")]
    public IResult Restricted()
    {
        return Results.Ok("access granted");
    }

    [HttpPost("Register")]
    public async Task<IResult> Register(
        [FromBody] UserRegister user
    ) {
        Console.WriteLine(user);

        return await this._userService.Register(user);
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
