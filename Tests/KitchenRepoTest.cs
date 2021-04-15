using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories;
using Xunit;

namespace Tests
{

    public class KitchenRepoTest
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid())
            .Options;

        [Fact]
        public async Task GetReviews()
        {
            var reviews = new List<Review>();
            reviews.Add(new Review(){RecipeId = 32,ReviewId = 3,ReviewDescription = "Tacos"});
            reviews.Add(new Review(){RecipeId = 329,ReviewId = 3,ReviewDescription = "TacoBurrito"});
            reviews.Add(new Review(){RecipeId = 320,ReviewId = 3,ReviewDescription = "Tacos"});

            var result1 = new List<Review>();
            var result2 = new List<Review>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenRepository(context);
                result1 = await msr.GetReviewsByRecipeId(3);
                
            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                result2 =  await context.Reviews.Where(r => r.RecipeId == 3)
                    .Include(r => r.User)
                    .ToListAsync();
            }
            Assert.Equal(result1, result2);
        }
        [Fact]
        public  void GetUserById()
        {
            var user = new User() { UserId = 4,Username = "Anis"};

            var result1 = new User();
            var result2 = new User();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var msr = new KitchenRepository(context);
                result1 =  msr.GetUserDataById(user.UserId);

            }
            using(var context2 = new InTheKitchenDBContext(testOptions))
            {
                context2.Database.EnsureCreated();
                result2 = context2.Users.FirstOrDefault(u => u.UserId == user.UserId);
            }
            Assert.Equal(result1, result2);
        }
        [Fact]
        public  void GetUserBySub()
        {
            var user = new User() { UserId = 4,Username = "Anis",Auth0 = "Stuff"};

            var result1 = new User();
            var result2 = new User();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var msr = new KitchenRepository(context);
                result1 =  msr.GetUserDataBySub(user.Auth0);

            }
            using(var context2 = new InTheKitchenDBContext(testOptions))
            {
                context2.Database.EnsureCreated();
                result2 = context2.Users.FirstOrDefault(u => u.Auth0 == user.Auth0);
            }
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void SaveUser()
        {
            var user = new User() { UserId = 4,Username = "Anis",Auth0 = "Stuff"};
            
            var result1 = new User();
            var result2 = new User();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var msr = new KitchenRepository(context);
                var newUser = msr.SaveNewUser(user,out result1);
                Assert.True(newUser);
            }
        }

        [Fact]
        public void  GetRecipes()
        {
            var reviews = new List<Recipe>();
            reviews.Add(new Recipe(){RecipeId = 32,RecipeAuthor = "Anis",RecipeDescription = "Tacos"});
            reviews.Add(new Recipe(){RecipeId = 320,RecipeAuthor = "Anis",RecipeDescription = "Tacos"});
          

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenRepository(context);
                result1 = (List<Recipe>) msr.GetAllRecipes();

            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                result2 =  context.Recipes
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                    .Include(r => r.Steps)
                    .ToList();
            }
            Assert.Equal(result1, result2);
        }
        [Fact]
        public void  GetRecipeByID()
        {
            var recipe = new Recipe() {RecipeId = 4, RecipeAuthor = "Anis"};
          

            var result1 = new Recipe();
            var result2 = new Recipe();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenRepository(context);
                result1 = msr.GetRecipeById(recipe.RecipeId);

            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                result2 =  context.Recipes.Where(r => r.RecipeId == recipe.RecipeId)
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                    .Include(r => r.Steps)
                    .Include(r => r.Reviews)
                    .FirstOrDefault();
            }
            Assert.Equal(result1, result2);
        }

    }
}
