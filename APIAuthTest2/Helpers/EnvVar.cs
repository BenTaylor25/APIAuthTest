
using APIAuthTest2.Constants;
using DotNetEnv;

namespace APIAuthTest2.Helpers;

public class EnvVar
{
    public static string GetVariable(string name)
    {
        Env.Load();

        string? value = Environment.GetEnvironmentVariable(name);

        if (value == null)
        {
            throw new InvalidOperationException(
                CONST_ENV.VAR_NOT_FOUND_ERR(name)
            );
        }

        return value;
    }
}
