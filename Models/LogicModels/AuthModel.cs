using System;
using System.Security.Cryptography;
using Repository.Helpers;
using Repository.Models;

namespace Models.LogicModels
{
    public class AuthModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Password { get; set; }

        public User GetMappedUser()
        {
            var hash = new PasswordManager().Hash(this.Password);

            User u1 = new User();
            u1.Username = this.Username;
            u1.Firstname = this.FirstName;
            u1.Lastname = this.LastName;
            u1.Email = this.Email;
            u1.PasswordHash = hash.HashedPass;
            u1.PasswordSalt = hash.Salt;
            u1.DateLastAccessed = TimeManager.GetTimeNow();
            return u1;
        }

        public bool HasSamePassword(string dbHashedPass)
        {
            return new PasswordManager().ComparePass(dbHashedPass, this.Password);
        }

        public static AuthModel GetFromUser(User user)
        {
            return new AuthModel()
            {
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Email = user.Email,
                Password = user.PasswordHash,
                ProfileImage = user.ImageUrl,
                Username = user.Username
            };
        }

    }

}