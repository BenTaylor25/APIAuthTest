
using ErrorOr;

public class UserModel
{
    public Guid Id { get; set;}
    public string Username { get; set;}
    public string Password { get; set;}

    private UserModel(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
    }

    public static ErrorOr<UserModel> Create(
        string username,
        string password
    )
    {
        bool usernameTaken = false;

        bool isValid =
            username.Length > 0 &&
            password.Length > 0 &&
            !usernameTaken;

        if (!isValid)
        {
            return Error.Validation();
        }

        UserModel user = new(username, password);
        return user;
    }
}
