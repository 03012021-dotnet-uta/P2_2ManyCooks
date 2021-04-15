using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Service.Helpers
{
    public class Auth0HttpRequestHandler
    {
        private IConfiguration _configuration;
        string baseUrl = "";
        public Auth0HttpRequestHandler(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            baseUrl = $"https://{_configuration["Auth0:Domain"]}";
        }
        public async Task<IRestResponse> Sendrequest(string urlExtension, Method method, string token, bool change = false)
        {
            string temp = "";
            if (change)
            {
                System.Console.WriteLine("before");
                System.Console.WriteLine(_configuration["Auth0:Audience"]);
                temp = _configuration["Auth0:Audience"];
                _configuration["Auth0:Audience"] = "https://dev-yktazjo3.us.auth0.com/api/v2/";
                System.Console.WriteLine("after");
                System.Console.WriteLine(_configuration["Auth0:Audience"]);
                // baseUrl = _configuration["Auth0:Audience"];
            }
            System.Console.WriteLine("baseUrl+urlExtension");
            System.Console.WriteLine(baseUrl + urlExtension);
            var client = new RestClient(baseUrl + urlExtension);
            client.Timeout = -1;
            var request = new RestRequest(method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            IRestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine("response.Content");
            Console.WriteLine(response.Content);
            if (change)
            {
                _configuration["Auth0:Audience"] = temp;
            }
            return response;
        }
    }
}