using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using APIAuthTest.Controllers.RequestBodies;
using APIAuthTest.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;

namespace APIAuthTest.Controllers;

public class AuthController : AppControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
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
        UserModel user = serviceResponse.Value;

        string token = _authService.GenerateJwt(user);
        return Ok(token);
    }
}
