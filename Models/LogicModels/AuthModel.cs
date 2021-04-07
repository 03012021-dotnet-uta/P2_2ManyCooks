using System;
using System.Security.Cryptography;
using Repository.Models;

namespace Models.LogicModels
{
    public class AuthModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
            u1.DateCreated = TimeManager.GetTimeNow();
            u1.DateLastAccessed = TimeManager.GetTimeNow();
            return u1;
        }

        public bool IsPasswordSame(string savedHash)
        {
            return new PasswordManager().ComparePass(savedHash, this.Password);
        }

    }

}