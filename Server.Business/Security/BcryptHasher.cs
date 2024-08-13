

namespace Server.Business.Security;

public static class BcryptHasher
{
    public static string HashPassword(string password)
    {
        if (password is null)
        {
            throw new ArgumentNullException("password");
        }

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string inputPassword,string userPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, userPassword);
    }
}
