using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using APIAuthTest.Controllers.RequestBodies;
using APIAuthTest.Services.AuthServices;

namespace APIAuthTest.Controllers;

public class AuthController : AppControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(
        [FromBody] LoginPostBody body
    )
    {
        ErrorOr<UserModel> serviceResponse = _authService.ValidateUser(
            body.Username,
            body.Password
        );

        if (serviceResponse.IsError)
        {
            return Unauthorized();
        }

        // Generate JWT.
        return Ok(); // return token
    }
}
