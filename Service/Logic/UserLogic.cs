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



        // public async Task<string> testTokenAsync(string token)
        // {
        //     // POST https://dev-yktazjo3.us.auth0.com/tokeninfo
        //     // Content-Type: application/json
        //     // {
        //     // "id_token": "ID_TOKEN"
        //     // }
        //     // HttpClient client = new HttpClient();
        //     // var values = new Dictionary<string, string>
        //     // {
        //     //     { "id_token", token },
        //     // };

        //     // var content = new FormUrlEncodedContent(values);

        //     // var response = await client.PostAsync("https://dev-yktazjo3.us.auth0.com/tokeninfo", content);

        //     // var responseString = await response.Content.ReadAsStringAsync();


        //     // GET https://dev-yktazjo3.us.auth0.com/userinfo
        //     // Authorization: 'Bearer {ACCESS_TOKEN}'
        //     // HttpClient client = new HttpClient();
        //     // string bearer = "Bearer " + token.Split("|")[0];
        //     // client.DefaultRequestHeaders.Add("Authorization", value: bearer);
        //     // // var values = new Dictionary<string, string>
        //     // // {
        //     // //     { "id_token", token },
        //     // // };
        //     // // HttpWebRequest request = new HttpWebRequest(values);

        //     // // var content = new FormUrlEncodedContent(values);

        //     // // HttpGet get = new HttpGet(getURL);

        //     // var response = await client.GetAsync("https://dev-yktazjo3.us.auth0.com/userinfo");

        //     // var responseString = await response.Content.ReadAsStringAsync();
        //     // System.Console.WriteLine("from auth0");
        //     // System.Console.WriteLine(responseString);


        //     // HttpClient client = new HttpClient();
        //     // string bearer = "Bearer " + token.Split("|")[0];
        //     // client.DefaultRequestHeaders.Add("Authorization", value: bearer);
        //     // // client.
        //     // // var values = new Dictionary<string, string>
        //     // // {
        //     // //     { "id_token", token },
        //     // // };
        //     // // HttpWebRequest request = new HttpWebRequest(values);

        //     // // var content = new FormUrlEncodedContent(values);

        //     // // HttpGet get = new HttpGet(getURL);

        //     // var response = await client.GetAsync("https://dev-yktazjo3.us.auth0.com/userinfo");

        //     // var responseString = await response.Content.ReadAsStringAsync();
        //     // System.Console.WriteLine("from auth0");
        //     // System.Console.WriteLine(responseString);

        //     // GET https://dev-yktazjo3.us.auth0.com/authorize?
        //     // audience=API_IDENTIFIER&
        //     // scope=SCOPE&
        //     // response_type=code&
        //     // client_id=Lvg2fAG086FX1Bufj3Hnq16BgYZu0d6F&
        //     // redirect_uri=undefined&
        //     // state=STATE

        //     // string url = "https://dev-yktazjo3.us.auth0.com/authorize";

        //     // string domain = $"https://{_configuration["Auth0:Domain"]}/";

        //     // var client = new RestClient(url);
        //     // client.Timeout = -1;
        //     // var request = new RestRequest(Method.GET);
        //     // request.AddHeader("Content-Type", "application/json");
        //     // // request.AddHeader("Cookie", "__cfduid=d979449f01e3f8825981085a90a3259f31617877900; did=s%3Av0%3A9ae9d570-9855-11eb-b1ad-2dad63ec5fc1.6nunk1eWesNZ9mj2qCq3GL0CAch43fogWyRAEvVEys0; did_compat=s%3Av0%3A9ae9d570-9855-11eb-b1ad-2dad63ec5fc1.6nunk1eWesNZ9mj2qCq3GL0CAch43fogWyRAEvVEys0");
        //     // request.AddParameter("response_type", "code");
        //     // // request.AddParameter("scope", "profile%email");
        //     // // request.AddParameter("grant_type", "authorization_code");
        //     // // request.AddParameter("audience", _configuration["Auth0:Audience"]);
        //     // request.AddParameter("client_id", _configuration["Auth0:client_id"]);
        //     // // request.AddParameter("redirect_uri", "http://localhost:4200/");
        //     // IRestResponse response = client.Execute(request);
        //     // Console.WriteLine(response.Content);


        //     string url = $"https://{_configuration["Auth0:Domain"]}/userinfo";

        //     // string domain = $"https://{_configuration["Auth0:Domain"]}/";


        //     // var splitToken = token.Split("|")[1];
        //     // System.Console.WriteLine("split token: " + splitToken);
        //     var client = new RestClient(url);
        //     client.Timeout = -1;
        //     var request = new RestRequest(Method.GET);
        //     request.AddHeader("Content-Type", "application/json");
        //     // request.AddHeader("Authorization", "Bearer " + splitToken);
        //     request.AddHeader("Authorization", token);
        //     // request.AddHeader("Cookie", "__cfduid=d979449f01e3f8825981085a90a3259f31617877900; did=s%3Av0%3A9ae9d570-9855-11eb-b1ad-2dad63ec5fc1.6nunk1eWesNZ9mj2qCq3GL0CAch43fogWyRAEvVEys0; did_compat=s%3Av0%3A9ae9d570-9855-11eb-b1ad-2dad63ec5fc1.6nunk1eWesNZ9mj2qCq3GL0CAch43fogWyRAEvVEys0");
        //     // request.AddParameter("response_type", "code");
        //     // request.AddParameter("scope", "profile%email");
        //     // request.AddParameter("grant_type", "authorization_code");
        //     // request.AddParameter("audience", _configuration["Auth0:Audience"]);
        //     // request.AddParameter("client_id", _configuration["Auth0:client_id"]);
        //     // request.AddParameter("redirect_uri", "http://localhost:4200/");
        //     IRestResponse response = client.Execute(request);
        //     System.Console.WriteLine("status: " + response.ResponseStatus);
        //     Console.WriteLine(response.Content);
        //     System.Console.WriteLine(response.ErrorMessage);
        //     System.Console.WriteLine(response.IsSuccessful);

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
