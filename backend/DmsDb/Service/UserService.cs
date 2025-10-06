using DmsDb.Object;
using DmsDb.Repository;
using DmsDb.Entity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace DmsDb.Service;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly JwtOptions _jwtOptions;

    public UserService(
            UserRepository userRepository,
            JwtOptions jwtOptions
    ) {
        this._userRepository = userRepository;
        this._jwtOptions = jwtOptions;
    }

    public async Task<(string?, UserReturn?)> Login(string login, string password)
    {
        UserEntity? user = await this._userRepository.GetByLogin(login);

        if (user == null)
            return (null, null);

        bool isValid = UserService.Verify(password, user.Password);

        if (!isValid)
            return (null, null);

        string jwtToken = this.GenerateJwtToken(user);
        UserReturn userToReturn = this.Convert(user);

        return (jwtToken, userToReturn);
    }

    public async Task<string?> Register(UserRegister user)
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

        if (!isCreated)
            return null;

        string jwtToken = this.GenerateJwtToken(userToCreate);
        return jwtToken;
    }

    private string GenerateJwtToken(UserEntity user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Role", user.Role)
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ValidityDurationMinutes)
        );

        string JwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return JwtTokenString;
    }

    private static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    private static bool Verify(string password, string passwordHashed)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
    }

    public UserReturn Convert(UserEntity userEntity)
    {
        UserReturn user = new UserReturn
        {
            Id = userEntity.Id,
            Login = userEntity.Login,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            Sex = userEntity.Sex,
            Role = userEntity.Role,
            PfpPath = userEntity.PfpPath
        };

        return user;
    }
}
