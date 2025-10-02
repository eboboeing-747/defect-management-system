using DmsDb.Entity;
using DmsDb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DmsApi.Controller;

[ApiController]
[Route("[controller]")]
public class UserController
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userController)
    {
        this._userRepository = userController;
    }

    [HttpGet("Test")]
    public async Task<IResult> Test()
    {
        return Results.Ok("ok");
    }

    [HttpPost("Register")]
    public async Task<IResult> Register(
            [FromBody] UserEntity user
    ) {
        Console.WriteLine(user);

        bool isCreated = await this._userRepository.Create(user);

        return isCreated ? Results.Ok() : Results.Conflict<string>("user with such login already exists");
    }
}
