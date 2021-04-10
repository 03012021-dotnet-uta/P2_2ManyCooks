using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Repository.Repositories;
using Models.LogicModels;

namespace Service.Logic
{
    public class UserLogic : IUserLogic
    {

        private InTheKitchenDBContext _context;
        private readonly KitchenRepository _repo;
        public UserLogic(InTheKitchenDBContext _context, KitchenRepository _repo)
        {
            this._context = _context;
            this._repo = _repo;
        }

        public List<User> getAllUsers()
        {
            // System.Web.HttpUtility..GetTokenAsync("Bearer", "access_token");
            return _context.Users.FromSqlRaw("Select * From Users").ToList();
        }

        public AuthModel UpdateUser(AuthModel authModel, Dictionary<string, string> userDictionary)
        {
            System.Console.WriteLine("update logic: dictionary:");
            System.Console.WriteLine(userDictionary);
            User user = authModel.GetMappedUser();
            AuthModel model;
            if (_repo.DoesUserExist(userDictionary["sub"]))
            {
                model = AuthModel.GetFromUser(_repo.UpdateUserData(user));
            }
            else
            {
                CreateNewUser(authModel, userDictionary, out model);
            }
            return model;
        }

        public bool CheckIfNewUser(Dictionary<string, string> userDataDictionary, out AuthModel authModel)
        {
            var success = false;

            //* Extract the data dictionary

            // * {"sub":"auth0|606e2aeaa32e9700697566ba",
            // * "nickname":"noureldinashraf6",
            // * "name":"noureldinashraf6@gmail.com",
            // * "picture":"https://s.gravatar.com/avatar/162c31c37ca4c96d5bf9e43b9e2bd5a0?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fno.png",
            // * "updated_at":"2021-04-08T13:21:23.390Z",
            // * "email":"noureldinashraf6@gmail.com",
            // * "email_verified":false}

            System.Console.WriteLine(userDataDictionary["sub"]);

            //* Check if this entry exists in our DB
            if (_repo.DoesUserExist(userDataDictionary["sub"]))
            {
                System.Console.WriteLine("user exists");
                //* If exists update user data
                success = UpdateUserDataDictionary(userDataDictionary);
                if (!success)
                {
                    System.Console.WriteLine("error updating user info");
                }
                authModel = GetCurrentUserData(userDataDictionary["sub"]);
                return success;
            }
            else
            {
                System.Console.WriteLine("user does not exist, returning false");
                //* If not return false to redirect to registration form
                authModel = new AuthModel();
                return false;
            }
        }

        public bool CreateNewUser(AuthModel authModel, Dictionary<string, string> userDictionary, out AuthModel newModel)
        {
            System.Console.WriteLine("update logic: dictionary:");
            System.Console.WriteLine(userDictionary);
            System.Console.WriteLine("userDictionary[\"email\"]");
            System.Console.WriteLine(userDictionary["email"]);
            // todo: if we give user more data, assign here
            User user = authModel.GetMappedUser();
            User authUser = _getUpdatedUserFromDictionary(userDictionary);
            authUser.Firstname = user.Firstname;
            authUser.Lastname = user.Lastname;
            authUser.DateCreated = DateTime.Now;
            authUser.PasswordHash = "";
            authUser.PasswordSalt = "";
            authUser.Username = authUser.Email;
            bool success = _repo.SaveNewUser(authUser, out user);
            if (success)
                newModel = AuthModel.GetFromUser(user);
            else
                newModel = null;
            return success;
        }

        private User _getUpdatedUserFromDictionary(Dictionary<string, string> userDictionary)
        {
            User user = new User();
            user.Email = userDictionary["email"];
            user.ImageUrl = userDictionary["picture"];
            user.Auth0 = userDictionary["sub"];
            // user.DateLastAccessed = TimeManager.GetTimeNow();
            user.DateLastAccessed = DateTime.Now;
            return user;
        }

        public bool UpdateUserDataDictionary(Dictionary<string, string> userData)
        {
            User user = _getUpdatedUserFromDictionary(userData);
            _repo.UpdateUserAuth0Data(user);
            return true;
        }

        public AuthModel GetCurrentUserData(string sub)
        {
            return AuthModel.GetFromUser(_repo.GetUserDataBySub(sub));
        }
    }
}
