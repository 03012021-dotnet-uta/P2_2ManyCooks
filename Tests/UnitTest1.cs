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
        public void TestListUser()
        {
            List<User> users = new List<User>();
            var user1 = (new User(){UserId=23,Firstname="Anis"});
             var user2 = (new User(){UserId=234,Firstname="Nour"});
            var user3 = (new User(){UserId=231,Firstname="Beau"});
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
           
            List<User> result1 = new List<User>();
            List<User> result2 = new List<User>();

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(user1);
                context.Add(user2);
                context.Add(user3);
                context.SaveChanges();

                result2 =  context.Users.ToList();
            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreated();
                var msr = new UserLogic(context);
                result1 =  msr.getAllUsers();
            }
          
            Assert.Equal(result1.Count,result2.Count);
        }
        [Fact]
        public async Task TestListTag()
        {
            var tags = new List<Tag>();
            var tag1 =(new Tag(){  TagId = 12, TagName = "Cheese",TagDescription = "Some description"});
            var tag2 = (new Tag(){  TagId = 120, TagName = "Beef",TagDescription = "Some other description"});
            tags.Add(tag1);
            tags.Add(tag2);
           
            var result1 = new List<Tag>();
            var result2 = new List<Tag>();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();
                result2 =  await context.Tags.ToListAsync();
            }
            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.geTags();
            }
            
            Assert.Equal(result1.Count,result2.Count);
        }
        [Fact]
        public async Task TestSingleIngredient()
        {
            var ingredient = new Ingredient()
            {
                IngredientId = 3,
                IngredientName = "Cheese",
                IngredientDescription = "svsvsvs",
                IngredientImage = "Some Image",
                RecipeIngredients = new List<RecipeIngredient>(),
                ThirdPartyApiId = "Google",
            };
            var result2 = new Ingredient();
            
             using (var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                context.Add(ingredient);
                context.SaveChangesAsync();
            }
            await using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreated();
                var msr = new ReviewStepTagLogic(context);
                result2 = await msr.getOneIngredientById(ingredient.IngredientId);
            }
            Assert.Equal(ingredient.IngredientDescription,result2.IngredientDescription);

        }
        [Fact]
        public async Task TestIngredient()
        {
            var ingredient = new Ingredient()
            {
                IngredientId = 34,
                IngredientName = "Cheese"
            };
            var result1 = new Ingredient();
            var result2 = new Ingredient();

             using(var context = new InTheKitchenDBContext(testOptions))
            {
                 context.Database.EnsureDeletedAsync();
                 context.Database.EnsureCreatedAsync();
                 context.Add(ingredient);
                 context.SaveChanges();
                result2 = await context.Ingredients.Where(i => i.IngredientName == ingredient.IngredientName).FirstOrDefaultAsync();
            } 

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);

                result1 = await msr.getOneIngredientByName(ingredient.IngredientName);
            }

           
            Assert.Equal(result1.IngredientName,result2.IngredientName);

        }
        [Fact]
        public async Task TestListIngredient()
        {
            var ingredients = new List<Ingredient>();
            var ing1 = new Ingredient(){  IngredientId = 12, IngredientName = "Cheese",IngredientDescription = "Some description"};
            var ing2 = new Ingredient(){  IngredientId = 120, IngredientName = "Beef",IngredientDescription = "Some other description"};
            ingredients.Add(ing2);
            ingredients.Add(ing1);
           
            var result1 = new List<Ingredient>();
           

             using(var context = new InTheKitchenDBContext(testOptions))
            {
                 context.Database.EnsureDeletedAsync();
                 context.Database.EnsureCreatedAsync();
                 context.Add(ing1);
                 context.Add(ing2);
                 context.SaveChanges();
            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                 context.Database.EnsureCreatedAsync();
                var msr = new ReviewStepTagLogic(context);
                result1 = await msr.getIngredients();
            }

           
            Assert.Equal(result1.Count,ingredients.Count);
        }

        [Fact]
        public async Task TestListReview()
        {
            var reviews = new List<Review>();
            var review1 = new Review(){  ReviewId = 12, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some description"};
            var review2 =  new Review(){  ReviewId = 120, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some other description"};
            reviews.Add(review1);
            reviews.Add(review2);

            using(var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add(review1);
                context.Add(review2);
                context.SaveChanges();
            }
            List<Review> result1;
            using(var context2 = new InTheKitchenDBContext(testOptions))
            {
                 context2.Database.EnsureCreated();
                var msr = new ReviewStepTagLogic(context2);
                result1 = await msr.getReviewsByRecipeName("Tacos");
            } 
           
            Assert.Equal(reviews.Count,result1.Count);
        }
        [Fact]
        public async Task TestListReviewByName()
        {
            var reviews = new List<Review>();
            var review1 =  new Review(){  ReviewId = 12, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some description",User = new User(){Firstname = "Anis"}};
            var review2 =  new Review(){  ReviewId = 120, Recipe = new Recipe(){ RecipeName = "Tacos"},ReviewDescription = "Some other description",User = new User(){Firstname = "Anis"}};
            reviews.Add(review1);
            reviews.Add(review2);
           
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

        [Fact]
        public async Task TestSingleTag()
        {
            {
                var tag = new Tag()
                {
                    TagId = 4,
                    TagName = "Chee",
                    TagDescription = "SomeSTuff",
                    RecipeTags = new List<RecipeTag>() { }
                };
                var result1 = new Tag();
                var result2 = new Tag();

                await using (var context = new InTheKitchenDBContext(testOptions))
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.EnsureCreatedAsync();
                    var msr = new KitchenLogic(context);
                    result1 = await msr.getOneTag(tag.TagName);

                }
                await using(var context2 = new InTheKitchenDBContext(testOptions))
                {
                    await context2.Database.EnsureCreatedAsync();
                    result2 = await context2.Tags.FirstOrDefaultAsync(t => t.TagName == tag.TagName);
                }
                Assert.Equal(result1,result2);
            }
        }
    }
}
