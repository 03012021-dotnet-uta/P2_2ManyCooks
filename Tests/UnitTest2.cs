using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.LogicModels;
using Repository.Models;
using Repository.Repositories;
using Service.Helpers;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest2
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid())
            .Options;

        [Fact]
        public async Task TestListRecipe()
        {
            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe() { RecipeId = 32, RecipeAuthor = "Anis", RecipeName = "Tacos" });
            ingredients.Add(new Recipe() { RecipeId = 329, RecipeAuthor = "Nour", RecipeName = "TacoBurrito" });
            ingredients.Add(new Recipe() { RecipeId = 320, RecipeAuthor = "Beau", RecipeName = "Tacos" });

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.getAllRecipe();
            }
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                result2 = await context.Recipes.ToListAsync();
            }
            Assert.Equal(result1, result2);
        }
        [Fact]
        public void TestListOfRecipeByName()
        {
            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe() { RecipeId = 32, RecipeAuthor = "Anis", RecipeName = "Tacos" });
            ingredients.Add(new Recipe() { RecipeId = 329, RecipeAuthor = "Nour", RecipeName = "TacoBurrito" });
            ingredients.Add(new Recipe() { RecipeId = 320, RecipeAuthor = "Beau", RecipeName = "Tacos" });

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = msr.getAllRecipeByRecipeName("Tacos");
            }
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                result2 = context.Recipes.Where(r => r.RecipeName == "Tacos").ToList();
            }
            Assert.Equal(result1, result2);
        }

        [Fact]
        public async Task TestListRecipeByTag()
        {
            var tag = new Tag() { TagId = 3, TagName = "Healthy" };
            var recipe = new Recipe() { RecipeId = 21, RecipeName = "Deli" };
            ICollection<RecipeTag> tags = new List<RecipeTag>();
            tags.Add(new RecipeTag() { Recipe = recipe, Tag = tag });
            tags.Add(new RecipeTag() { Recipe = recipe, Tag = tag });

            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe() { RecipeId = 32, RecipeAuthor = "Anis", RecipeName = "Tacos", RecipeTags = tags });
            ingredients.Add(new Recipe() { RecipeId = 329, RecipeAuthor = "Nour", RecipeName = "TacoBurrito", RecipeTags = tags });
            ingredients.Add(new Recipe() { RecipeId = 320, RecipeAuthor = "Beau", RecipeName = "Tacos", RecipeTags = tags });

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.getAllRecipeByTags("Spicy");
            }
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 = await context.Recipes.FromSqlRaw("$SELECT * FROM Recipes WHERE RecipeId IN (SELECT RecipeId FROM RecipeTags WHERE TagId = 3)").ToListAsync();
            }
            Assert.Equal(result1, result2);
        }
        [Fact]
        public async Task TestRecipe()
        {
            var recipeResult = new Recipe()
            {
                RecipeId = 2377,
                RecipeName = "Cheese",
                RecipeAuthor = "Anis"
            };

            var result1 = new Recipe();
            var result2 = new Recipe();

            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.addNewRecipe(recipeResult);
            }
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureCreatedAsync();
                context.Recipes.AddAsync(recipeResult);
                context.SaveChangesAsync();
                result2 = await context.Recipes.Where(r => r.RecipeName == recipeResult.RecipeName).FirstOrDefaultAsync();
            }
            Assert.Equal(result1, result2);
        }

        [Fact]
        public async Task TestRecipeSaving()
        {
            // var tag = new Recipe() { TagId = 123, TagName = "Cheese" };
            var sentrecipe = new SentRecipe();
            var date = DateTime.Now;
            sentrecipe.DateCreated = date;
            sentrecipe.DateLastPrepared = date;
            Tag t = new Tag()
            {
                TagName = "tag name 2"
            };
            Ingredient i = new Ingredient()
            {
                IngredientName = "ing name 2"
            };
            Step s = new Step()
            {
                StepDescription = "new description",
                RecipeStepNo = 1
            };
            sentrecipe.ingredients = new List<Ingredient>(){
                new Ingredient() {IngredientName="ingredient name"}
            };
            sentrecipe.tags = new List<Tag>(){
                new Tag() {TagName="tag name"}
            };
            Step s1 = new Step()
            {
                StepDescription = "step description"
            };
            sentrecipe.Steps = new List<Step>() {
                s1
            };

            User user = new User()
            {
                Auth0 = "authcode",
                Firstname = "fname",
                Lastname = "lname",
                DateCreated = date,
                Email = "email",
                ImageUrl = "picture"
            };
            AuthModel model = new AuthModel()
            {
                Sub = "authcode",
                FirstName = "fname",
                LastName = "lname",
                Email = "email",
                ProfileImage = "picture",
                Username = "username"
            };

            Dictionary<string, string> userDictionary = new Dictionary<string, string>();
            userDictionary["email"] = "email";
            userDictionary["picture"] = "picture";
            userDictionary["sub"] = "authcode";


            bool result;
            Recipe recipe = null;
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var repo = new KitchenRepository(context);
                var msr = new KitchenLogic(context, repo);
                var reviewlogic = new ReviewStepTagLogic(context, repo);
                var userlogic = new UserLogic(context, repo);
                userlogic.UpdateUser(model, userDictionary);
                userlogic.UpdateUser(model, userDictionary);
                var sr = await msr.saveRecipe(sentrecipe, model.Sub);
                sentrecipe.tags.Add(t);
                sentrecipe.ingredients.Add(i);
                sentrecipe.Steps.Add(s);
                sentrecipe.Steps.Remove(s1);
                sr = await msr.saveRecipe(sentrecipe, model.Sub);
                recipe = repo.GetRecipeById(sr.RecipeId);
                repo.UpdateUserAuth0Data(user);
                var rev = new Review()
                {
                    Recipe = recipe,
                    ReviewDate = date,
                    ReviewDescription = "review description",
                    RecipeId = recipe.RecipeId,
                };
                reviewlogic.addReview(userDictionary["sub"], rev);
                HistoryModel hmodel = new HistoryModel()
                {
                    recipeId = recipe.RecipeId,
                    sub = model.Sub
                };
                msr.SaveRecipePrepare(hmodel);
            }
            Assert.Equal(recipe.RecipeTags.ToArray()[0].Tag.TagName, "tag name");
        }

        [Fact]
        public async Task TestUserLogic()
        {
            User user = new User() { Auth0 = "authcode", Firstname = "fname", Lastname = "lname" };
            AuthModel model = new AuthModel()
            {
                Sub = "authcode",
                FirstName = "fname",
                LastName = "lname",
                Email = "email",
                ProfileImage = "",
                Username = "username"
            };
            Dictionary<string, string> userDictionary = new Dictionary<string, string>();
            userDictionary["email"] = "email";
            userDictionary["picture"] = "picture";
            userDictionary["sub"] = "authcode";
            bool result;
            bool isnew = false;
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var repo = new KitchenRepository(context);
                var userlogic = new UserLogic(context, repo);
                AuthModel outModel = null;
                isnew = userlogic.CheckIfNewUser(userDictionary, out outModel);
                userlogic.GetAllUsers();
            }
            Assert.False(isnew);
        }

        [Fact]
        public async Task TestListTagByTagName()
        {
            var tag = new Tag() { TagId = 123, TagName = "Cheese" };
            bool result;
            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result = await msr.existTag(tag.TagName);
            }
            Assert.False(result);
        }

        [Fact]
        public void TestRecipeExist()
        {
            var recipe = new Recipe() { RecipeId = 13123, RecipeName = "Cheese" };



            using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                bool result = msr.existRecipeName(recipe.RecipeName);
                Assert.False(result);
            }
        }
    }
}
