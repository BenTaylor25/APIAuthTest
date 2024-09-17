
using APIAuthTest.Controllers.RequestBodies;
using ErrorOr;

namespace APIAuthTest.Services.AuthServices;

public interface IAuthService
{
    ErrorOr<UserModel> ValidateUser(string username, string password);

    string GenerateJwt(UserModel user);
}
