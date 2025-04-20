using System.Security.Cryptography;
using System.Text;

namespace NummyApi.Helpers;

public static class SecurityHelper
{
    public static (string Hash, string Salt) GeneratePasswordHash(string password)
    {
        // Generate a random salt
        byte[] saltBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        string salt = Convert.ToBase64String(saltBytes);

        // Combine password and salt
        string saltedPassword = password + salt;

        // Generate hash
        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            string hash = Convert.ToBase64String(hashBytes);
            return (hash, salt);
        }
    }

    public static bool ValidatePassword(string password, string storedHash, string storedSalt)
    {
        // Combine provided password with stored salt
        string saltedPassword = password + storedSalt;

        // Generate hash
        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            string hash = Convert.ToBase64String(hashBytes);

            // Compare with stored hash
            return hash == storedHash;
        }
    }
}