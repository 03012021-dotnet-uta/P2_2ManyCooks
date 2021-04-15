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
        public string ProfileImage { get; set; }
        public string Password { get; set; }
        public string Sub { get; set; }

        public User GetMappedUser()
        {


            User u1 = new User();
            u1.Username = this.Username;
            u1.Firstname = this.FirstName;
            u1.Lastname = this.LastName;
            u1.Email = this.Email;
            u1.PasswordHash = "";
            u1.PasswordSalt = "";

            u1.DateLastAccessed = DateTime.Now;
            u1.ImageUrl = this.ProfileImage;
            u1.Auth0 = this.Sub;
            return u1;
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
                Username = user.Username,
                Sub = user.Auth0,
            };
        }
    }

}