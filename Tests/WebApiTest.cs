
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using KitchenWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Models;
using Xunit;


namespace Tests
{

    public class WebApiTest :IDisposable
    {
        protected TestServer testServer;
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        public WebApiTest()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();
            testServer = new TestServer(webBuilder);
        }
        public void Dispose()
        {
           testServer.Dispose();
        }

        [Fact]
        public async Task TestReadTag()
        {
            var response = await testServer.CreateRequest("/tag").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }

        [Fact]
        public async Task TestReadIngredient()
        {
            var response = await testServer.CreateRequest("/ingredient").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async Task TestAddRecipe()
        {
            var reqest = new HttpRequestMessage(HttpMethod.Post, "/recipe");
            reqest.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"RecipeName","Anis"}
            }),Encoding.Default,"application/json");
            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(reqest);
            response = await testServer.CreateRequest("/recipeName/Anis").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }

        //[Fact]
        //public async Task TestReadRecipeById()
        //{
        //    var response = await testServer.CreateRequest("/Recipe/recipeById/3").SendAsync("GET");
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //}
        //[Fact]
        //public async Task TestReadRecipeByTag()
        //{
        //    var response = await testServer.CreateRequest("/tag/Spicy").SendAsync("GET");
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //}
        [Fact]
        public async Task TestReadRecipeApi()
        {
            var response = await testServer.CreateRequest("/api/Cheese").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestIngredient()
        {
            var response = await testServer.CreateRequest("/ingredient").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        //[Fact]
        //public async Task TestIngredientById()
        //{
        //    var response = await testServer.CreateRequest("byIngredientId/5").SendAsync("GET");
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}
        [Fact]
        public async Task TestReadRecipeByName()
        {
            var response = await testServer.CreateRequest("/byIngredientName/Butter").SendAsync("GET");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task TestReviewByRecipe()
        {
            var response = await testServer.CreateRequest("byRecipeName/Cheese%20Sandwitch").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }
        [Fact]
        public async Task TestReviewByUser()
        {
            var response = await testServer.CreateRequest("byUser/Anis").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }


    }
}
