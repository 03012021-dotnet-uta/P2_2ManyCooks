using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.LogicModels;
using Repository.Models;
using Repository.Repositories;
using RestSharp;
using Service.Authenticators;
using Service.Helpers;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest3
    {
        
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        //[Fact]
        //public async Task getSentRecipeByID()
        //{
        //    var reviews = new List<Review>();
        //    var review1 = new Review() { ReviewId = 12, Recipe = new Recipe() { RecipeId = 120, RecipeName = "Tacos" }, ReviewDescription = "Some description" };
        //    var review2 = new Review() { ReviewId = 12, Recipe = new Recipe() { RecipeId = 120, RecipeName = "Tacos" }, ReviewDescription = "Some other description" };
        //    using (var context = new InTheKitchenDBContext(testOptions))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        context.Add<Review>(review1);
        //        context.Add<Review>(review2);
        //        context.SaveChangesAsync();
        //    }
        //    Task<List<Review>> result1;
        //    using (var context2 = new InTheKitchenDBContext(testOptions))
        //    {
        //        context2.Database.EnsureCreated();
        //        var msr = new KitchenRepository(context2);
        //        result1 = msr.GetReviewsByRecipeId(120);
        //    }

        //    Assert.Equal(2, reviews.Count);
        //}
        //[Fact]
        //public async Task TestRviewadd()
        //{
        //    var user = new User()
        //    {
        //        UserId = 4,
        //        Auth0 = "AnisSub"
        //    };
        //    var review1 = new Review(){  ReviewId = 12, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some description"};
        //    var review2 =  new Review(){  ReviewId = 120, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some other description"};


           
        //    List<Review> result2;

        //    using(var context = new InTheKitchenDBContext(testOptions))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        context.Add(review1);
        //        context.Add(review2);
        //        context.SaveChanges();
        //        result2 =await context.Reviews.Where(r => r.RecipeId == review1.RecipeId).Include(r => r.User).ToListAsync();
        //    }
        //    Task<List<Review>> result1;
        //    using(var context2 = new InTheKitchenDBContext(testOptions))
        //    {
        //        context2.Database.EnsureCreated();
        //        var msr = new KitchenRepository(context2);
        //        result1 = msr.AddNewReview(user.Auth0, review1);
        //    } 
           
        //    Assert.Equal(result1.Result,result2);

        //}
        [Fact]
        public async Task AuthStuff()
        {
            string token = "12345";
            var authenticator = new Authenticator();
            var answer = authenticator.GetUserAuth0Dictionary(token);

            Assert.NotNull(answer);
        }
        
    }
}
