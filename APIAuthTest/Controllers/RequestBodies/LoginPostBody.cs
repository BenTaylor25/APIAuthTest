
namespace APIAuthTest.Controllers.RequestBodies;

public class LoginPostBody
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginPostBody(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
