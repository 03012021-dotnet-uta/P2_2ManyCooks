using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Helpers;
using Repository.Models;

namespace Repository.Repositories
{
    public class KitchenRepository
    {
        private InTheKitchenDBContext _context;

        public KitchenRepository(InTheKitchenDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Checks if user exists in our DB
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if user exists</returns>
        public bool DoesUserExist(string Auth0)
        {
            return _context.Users.Any(u => u.Auth0 == Auth0);
        }

        public bool UpdateUserAuth0Data(Dictionary<string, string> data)
        {
            // todo: talk about what we want to save
            // {"sub":"auth0|606e2aeaa32e9700697566ba",
            // "nickname":"noureldinashraf6",
            // "name":"noureldinashraf6@gmail.com",
            // "picture":"https://s.gravatar.com/avatar/162c31c37ca4c96d5bf9e43b9e2bd5a0?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fno.png",
            // "updated_at":"2021-04-08T13:21:23.390Z",
            // "email":"noureldinashraf6@gmail.com",
            // "email_verified":false}
            var user = _context.Users.Where(u => u.Auth0 == data["sub"]).FirstOrDefault();

            user.Email = data["email"];
            user.ImageUrl = data["picture"];
            user.DateLastAccessed = TimeManager.GetTimeNow();
            return true;
        }

        public User GetUserDataById(int id)
        {
            return _context.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public User GetUserDataBySub(string sub)
        {
            return _context.Users.Where(u => u.Auth0 == sub).FirstOrDefault();
        }
    }
}