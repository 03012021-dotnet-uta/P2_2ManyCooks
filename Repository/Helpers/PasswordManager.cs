using System;
using System.Security.Cryptography;

public class PasswordManager
{
    /// <summary>
    /// Hash the entered password that is in visible string
    /// to a hashed version ready to store in db
    /// </summary>
    /// <param name="pass"></param>
    /// <returns>hashed password</returns>
    public dynamic Hash(string pass)
    {
        // STEP 1 Create the salt value with a cryptographic PRNG:
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        // STEP 2 Create the Rfc2898DeriveBytes and get the hash value:
        var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        // STEP 3 Combine the salt and password bytes for later use:
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        // STEP 4 Turn the combined salt+hash into a string for storage
        return new { HashedPass = Convert.ToBase64String(hashBytes), Salt = Convert.ToBase64String(salt) };
    }

    /// <summary>
    /// compare a saved hashed password with another
    /// just entered in pure visible string
    /// method hashes the entered pass and 
    /// compares it with the saved hashed passwords
    /// </summary>
    /// <param name="savedHash"></param>
    /// <param name="entered"></param>
    /// <returns>true if matching</returns>
    public bool ComparePass(string savedHash, string entered)
    {
        /* Extract the bytes */
        byte[] hashBytes = Convert.FromBase64String(savedHash);
        /* Get the salt */
        byte[] salt = new byte[16];
        lock (salt)
        {
            lock (hashBytes)
            {
                lock (this)
                {
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                }
            }
        }
        /* Compute the hash on the password the user entered */
        var pbkdf2 = new Rfc2898DeriveBytes(entered, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        /* Compare the results */
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }

        return true;
    }
}
