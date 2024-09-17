
using ErrorOr;

namespace APIAuthTest.Services.AuthServices;

public class AuthServices : IAuthService
{
    private readonly List<UserModel> _users;

    public AuthServices()
    {
        _users = new List<UserModel>();
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
}
