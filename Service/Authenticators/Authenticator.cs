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

        public Dictionary<string, string> GetUserAuth0Dictionary(string token)
        {
            var success = false;
            // System.Console.WriteLine("token");
            // System.Console.WriteLine(token);

            //* Get the data from Auth0
            string url = $"https://{_configuration["Auth0:Domain"]}/userinfo";

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            IRestResponse response = client.Execute(request);
            // System.Console.WriteLine("status: " + response.ResponseStatus);
            System.Console.WriteLine("user data:");
            Console.WriteLine(response.Content);
            System.Console.WriteLine("response status");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.IsSuccessful);
            // Console.WriteLine(response.ResponseStatus);

            // success = response.ResponseStatus.Equals("True");
            // System.Console.WriteLine("succes: " + success);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
        }
    }
}