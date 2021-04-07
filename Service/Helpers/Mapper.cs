using System.Security.Cryptography;
using System.Text;
using Models.LogicModels;
using Repository.Models;

namespace Service.Helpers
{
    public class Mapper
    {
        public User GetUser(AuthModel authModel)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                User u1 = new User()
                {
                    Email = authModel.Email,
                    Firstname = authModel.FirstName,
                    Lastname = authModel.LastName,
                    Username = authModel.Username,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authModel.Password)).ToString(),//this returns a byte[] representing the password
                    PasswordSalt = hmac.Key.ToString()     // this assigns the randomly generated Key (comes with the HMAC instance) to the salt variable of the user instance,
                };

                return u1;
            }
        }
    }
}