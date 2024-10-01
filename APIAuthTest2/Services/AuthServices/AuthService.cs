using System.Text;
using Microsoft.IdentityModel.Tokens;

using ErrorOr;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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
        SymmetricSecurityKey securityKey = new(
            Encoding.UTF8.GetBytes("changemechangemechangemechangeme")
        );

        SigningCredentials credentials = new(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        };

        JwtSecurityToken token = new(
            "APIAuthTest",   // move this to .env
            "APIAuthTest?",   // move this to .env
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
