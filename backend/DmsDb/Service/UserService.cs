using DmsDb.Object;
using DmsDb.Repository;
using DmsDb.Entity;
using Microsoft.AspNetCore.Http;

namespace DmsDb.Service;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public async Task<IResult> Register(UserRegister user)
    {
        UserEntity userToCreate = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = user.Login,
            Password = UserService.Hash(user.Password),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Sex = user.Sex,
            Role = "engineer" // add a check for role existence
        };

        bool isCreated = await this._userRepository.Create(userToCreate);

        if (isCreated)
            return Results.Created();

        return Results.Conflict($"{{\"error\": \"user with this login already exists\"}}");
        // return Results.Json(new Dictionary<string, string>)
    }

    private static string Hash(string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        return hashedPassword;
    }

    private static bool Verify(string password, string passwordHashed)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
    }
}
