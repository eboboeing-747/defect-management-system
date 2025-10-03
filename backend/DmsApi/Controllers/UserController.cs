using DmsDb.Entity;
using DmsDb.Service;
using DmsDb.Object;
using Microsoft.AspNetCore.Mvc;

namespace DmsApi.Controller;

[ApiController]
[Route("[controller]")]
public class UserController
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

    [HttpPost("Register")]
    public async Task<IResult> Register(
            [FromBody] UserRegister user
    ) {
        Console.WriteLine(user);

        return await this._userService.Register(user);
    }
}
