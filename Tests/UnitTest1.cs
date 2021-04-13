using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenWeb.Controllers;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;


        [Fact]
        public void Test2()
        {
            List<User> users = new List<User>();
            users.Add(new User(){UserId=23,Firstname="Anis"});
            users.Add(new User(){UserId=234,Firstname="Nour"});
            users.Add(new User(){UserId=231,Firstname="Beau"});
           
            List<User> result1 = new List<User>();
            List<User> result2 = new List<User>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var msr = new UserLogic(context);
                result1 =  msr.getAllUsers();
            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreated();
                result2 =  context.Users.ToList();
            }
            Assert.Equal(result1,result2);
        }
        [Fact]
        public async Task Test3Async()
        {
            var tags = new List<Tag>();
            tags.Add(new Tag(){  TagId = 12, TagName = "Cheese",TagDescription = "Some description"});
            tags.Add(new Tag(){  TagId = 120, TagName = "Beef",TagDescription = "Some other description"});
            
           
            var result1 = new List<Tag>();
            var result2 = new List<Tag>();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.geTags();
            }
            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 =  await context.Tags.ToListAsync();
            }
            Assert.Equal(result1,result2);
        }
        [Fact]
        public async Task Test7Async()
        {
            var ingredient = new Ingredient()
            {
                IngredientId = 34,
                IngredientName = "Cheese"
            };
            var result1 = new Ingredient();
            var result2 = new Ingredient();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);

                result1 = await msr.getOneIngredientById(ingredient.IngredientId);
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 = await context.Ingredients.FindAsync(ingredient.IngredientId);
            }
            Assert.Equal(result1,result2);

        }
        [Fact]
        public async Task Test8Async()
        {
            var ingredient = new Ingredient()
            {
                IngredientId = 34,
                IngredientName = "Cheese"
            };
            var result1 = new Ingredient();
            var result2 = new Ingredient();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);

                result1 = await msr.getOneIngredientByName(ingredient.IngredientName);
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 = await context.Ingredients.Where(i => i.IngredientName == ingredient.IngredientName).FirstOrDefaultAsync();
            }
            Assert.Equal(result1,result2);

        }
        [Fact]
        public async Task Test4Async()
        {
            var ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient(){  IngredientId = 12, IngredientName = "Cheese",IngredientDescription = "Some description"});
            ingredients.Add(new Ingredient(){  IngredientId = 120, IngredientName = "Beef",IngredientDescription = "Some other description"});
            
           
            var result1 = new List<Ingredient>();
            var result2 = new List<Ingredient>();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.getIngredients();
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 =  await context.Ingredients.ToListAsync();
            }
            Assert.Equal(result1,result2);
        }

        [Fact]
        public async Task Test5Async()
        {
            var reviews = new List<Review>();
            reviews.Add(new Review(){  ReviewId = 12, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some description"});
            reviews.Add(new Review(){  ReviewId = 120, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some other description"});
            
           
            var result1 = new List<Review>();
          
            var result2 = new List<Review>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.getReviewsByRecipeName("Tacos");

              
            }

            await using(var context2 = new InTheKitchenDBContext(testOptions))
            {
                await context2.Database.EnsureCreatedAsync();
                result2 = await context2.Reviews.Include(r => r.Recipe).Where(r=> r.Recipe.RecipeName == "Tacos").ToListAsync();

            }
            Assert.Equal(result1,result2);
        }
        [Fact]
        public async Task Test6Async()
        {
            var reviews = new List<Review>();
            reviews.Add(new Review(){  ReviewId = 12, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some description",User = new User(){Firstname = "Anis"}});
            reviews.Add(new Review(){  ReviewId = 120, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some other description",User = new User(){Firstname = "Anis"}});
            
           
            var result1 = new List<Review>();
          
            var result2 = new List<Review>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.getReviewByUser("Anis");

              
            }

            await using(var context2 = new InTheKitchenDBContext(testOptions))
            {
                await context2.Database.EnsureCreatedAsync();
                result2 = await context2.Reviews.Include(r => r.User).Where(r=> r.User.Firstname == "Anis").ToListAsync();

            }
            Assert.Equal(result1,result2);
        }

    }
}
