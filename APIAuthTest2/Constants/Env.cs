
namespace APIAuthTest2.Constants;

public static class CONST_ENV
{
    public const string API_KEY = "API_KEYs";

    public static string VAR_NOT_FOUND_ERR(string var_name)
    {
        return $"Failed to load Environment variable '{var_name}'.";
    }
}

