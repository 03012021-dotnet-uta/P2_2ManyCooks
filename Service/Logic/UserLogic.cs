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

namespace Service.Logic
{
    public class UserLogic : IUserLogic
    {

        private InTheKitchenDBContext _context;
        public UserLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }

        public List<User> getAUsers()
        {
            // System.Web.HttpUtility..GetTokenAsync("Bearer", "access_token");
            return _context.Users.FromSqlRaw("Select * From Users").ToList();
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

        //     var client2 = new RestClient("https://dev-yktazjo3.us.auth0.com/oauth/token");
        //     var request2 = new RestRequest(Method.POST);
        //     request.AddHeader("content-type", "application/json");
        //     request.AddParameter("application/json", "{\"client_id\":\"B7WJeAtwcYChReVTSo6Y8Ke93uTTzIKQ\",\"client_secret\":\"q2fdxgFehPAO4FaFzg2F4XeZ1Dzwabd7YxpsSbOqmq1Bfvx1NDBR6GmoOcWh2hWr\",\"audience\":\"https://dev-yktazjo3.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
        //     IRestResponse respons2e = client2.Execute(request2);
        //     System.Console.WriteLine();
        //     System.Console.WriteLine("status: " + respons2e.ResponseStatus);
        //     Console.WriteLine(respons2e.Content);
        //     System.Console.WriteLine(respons2e.ErrorMessage);
        //     System.Console.WriteLine(respons2e.IsSuccessful);

        //     return response.Content;

        // }

    }
}
