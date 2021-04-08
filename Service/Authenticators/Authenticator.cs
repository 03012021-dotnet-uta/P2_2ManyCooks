using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Service.Interfaces;
using System.Collections.Generic;
using Repository.Repositories;
using Repository.Models;
using Models.LogicModels;

namespace Service.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IConfiguration _configuration;
        private readonly KitchenRepository _repo;

        public Authenticator(IConfiguration _configuration, KitchenRepository _repo)
        {
            this._configuration = _configuration;
            this._repo = _repo;
        }

        /// <summary>
        /// Gets the token which is the bearer token from Angular. <br/>
        /// Then makes a request to the Auth0 API to get the user info. <br/>
        /// Then checks our DB if the user exists or not. <br/>
        /// If this method returns false we should redirect to a form that allows the user to enter his missing data
        /// </summary>
        /// <param name="token"></param>
        /// <returns>True if user exists in DB</returns>
        public bool CheckIfNewUser(string token)
        {
            var success = false;

            //* Get the data from Auth0
            string url = $"https://{_configuration["Auth0:Domain"]}/userinfo";

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            IRestResponse response = client.Execute(request);
            System.Console.WriteLine("status: " + response.ResponseStatus);
            System.Console.WriteLine("user data:");
            Console.WriteLine(response.Content);

            success = response.ResponseStatus.Equals("True");
            if (!success)
            {
                System.Console.WriteLine("error retrieving from Auth0");
                return false;
            }

            //* Extract the data dictionary
            var userDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            // {"sub":"auth0|606e2aeaa32e9700697566ba",
            // "nickname":"noureldinashraf6",
            // "name":"noureldinashraf6@gmail.com",
            // "picture":"https://s.gravatar.com/avatar/162c31c37ca4c96d5bf9e43b9e2bd5a0?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fno.png",
            // "updated_at":"2021-04-08T13:21:23.390Z",
            // "email":"noureldinashraf6@gmail.com",
            // "email_verified":false}
            System.Console.WriteLine(userDataDictionary["sub"]);

            //* Check if this entry exists in our DB
            if (!_repo.DoesUserExist(userDataDictionary["sub"]))
            {
                //* If exists update user data
                success = UpdateUserData(userDataDictionary);
                if (!success)
                {
                    System.Console.WriteLine("error updating user info");

                }
                return success;
            }
            else
            {
                //* If not return false to redirect to registration form
                return false;
            }
        }

        /// <summary>
        /// Can only use this method if User is already saved in our DB. <br/>
        /// Recieves the dictionary with all user data from Auth0.
        /// Then uses it to Update the information in our DB in case user changed their data. <br/>
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>True if success</returns>
        public bool UpdateUserData(Dictionary<string, string> userData)
        {
            _repo.UpdateUserAuth0Data(userData);
            return true;
        }


        public AuthModel GetCurrentUserData(string sub)
        {
            return AuthModel.GetFromUser(_repo.GetUserDataBySub(sub));
        }
    }
}