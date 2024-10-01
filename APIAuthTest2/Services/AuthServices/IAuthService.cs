
using ErrorOr;

namespace APIAuthTest2.Services.AuthServices;

public interface IAuthService
{
    ErrorOr<UserModel> ValidateUser(string username, string password);

    string GenerateJwt(UserModel user);
}
