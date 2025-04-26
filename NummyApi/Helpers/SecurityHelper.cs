using System.Security.Cryptography;
using System.Text;

namespace NummyApi.Helpers;

public static class SecurityHelper
{
    public static (string Hash, string Salt) GeneratePasswordHash(string password)
    {
        // Generate a random salt
        var saltBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        var salt = Convert.ToBase64String(saltBytes);

        // Combine password and salt
        var saltedPassword = password + salt;

        // Generate hash
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            var hash = Convert.ToBase64String(hashBytes);
            return (hash, salt);
        }
    }

    public static bool ValidatePassword(string password, string storedHash, string storedSalt)
    {
        // Combine provided password with stored salt
        var saltedPassword = password + storedSalt;

        // Generate hash
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            var hash = Convert.ToBase64String(hashBytes);

            // Compare with stored hash
            return hash == storedHash;
        }
    }
}