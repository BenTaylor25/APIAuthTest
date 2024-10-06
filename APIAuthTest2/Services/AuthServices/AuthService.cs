using System.Text;
using Microsoft.IdentityModel.Tokens;

using ErrorOr;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using APIAuthTest2.Constants;
using DotNetEnv;
using APIAuthTest2.Helpers;

namespace APIAuthTest2.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly List<UserModel> _users;

    public AuthService()
    {
        _users = new List<UserModel>();

        ErrorOr<UserModel> userResponse = UserModel.Create("Ben", "password");
        if (!userResponse.IsError)
        {
            _users.Add(userResponse.Value);
        }
    }

    public ErrorOr<UserModel> ValidateUser(
        string username,
        string password
    )
    {
        foreach (UserModel user in _users)
        {
            if (user.Username == username)
            {
                if (user.Password == password)
                {
                    return user;
                }
                return Error.Validation("Authentication");
            }
        }
        return Error.NotFound();
    }

    public string GenerateJwt(UserModel user)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        string keyEnvVar = EnvVar.GetVariable(CONST_ENV.API_KEY);

        byte[]? key = Encoding.UTF8.GetBytes(keyEnvVar);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", "user_id")}),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}
