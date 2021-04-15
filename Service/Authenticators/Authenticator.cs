using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Service.Interfaces;
using System.Collections.Generic;
using Repository.Repositories;
using Repository.Models;
using Models.LogicModels;
using System.Linq;
using Service.Helpers;
using System.Threading.Tasks;

namespace Service.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IConfiguration _configuration;
        private readonly KitchenRepository _repo;
        private readonly Auth0HttpRequestHandler _handler;

        public Authenticator(IConfiguration _configuration, KitchenRepository _repo, Auth0HttpRequestHandler _handler)
        {
            this._configuration = _configuration;
            this._repo = _repo;
            this._handler = _handler;
        }


        public async Task<Dictionary<string, string>> GetUserAuth0Dictionary(string token)
        {
            var success = false;
            IRestResponse response = await _handler.Sendrequest("/userinfo", Method.GET, token);
            System.Console.WriteLine("user data:");
            Console.WriteLine(response.Content);
            System.Console.WriteLine("response status");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.IsSuccessful);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
        }
    }
}