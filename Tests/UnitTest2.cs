using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest2
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        [Fact]
        public async Task Test1Async()
        {
            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe(){RecipeId = 32,RecipeAuthor = "Anis",RecipeName = "Tacos"});
            ingredients.Add(new Recipe(){RecipeId = 329,RecipeAuthor = "Nour",RecipeName = "TacoBurrito"});
            ingredients.Add(new Recipe(){RecipeId = 320,RecipeAuthor = "Beau",RecipeName = "Tacos"});

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.getAllRecipe();
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 =  await context.Recipes.ToListAsync();
            }
            Assert.Equal(result1,result2);
        }
        [Fact]
        public void Test2Async()
        {
            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe(){RecipeId = 32,RecipeAuthor = "Anis",RecipeName = "Tacos"});
            ingredients.Add(new Recipe(){RecipeId = 329,RecipeAuthor = "Nour",RecipeName = "TacoBurrito"});
            ingredients.Add(new Recipe(){RecipeId = 320,RecipeAuthor = "Beau",RecipeName = "Tacos"});

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

             using(var context = new InTheKitchenDBContext(testOptions))
            {
                 context.Database.EnsureDeletedAsync();
                 context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 =  msr.getAllRecipeByRecipeName("Tacos");
            }
            using(var context = new InTheKitchenDBContext(testOptions))
            {
                 context.Database.EnsureCreatedAsync();
                result2 =  context.Recipes.Where(r =>r.RecipeName == "Tacos").ToList();
            }
            Assert.Equal(result1,result2);
        }

        [Fact]
        public async Task Test3Async()
        {
            var tag = new Tag() {TagId = 3, TagName = "Healthy"};
            var recipe = new Recipe() {RecipeId = 21, RecipeName = "Deli"};
            ICollection<RecipeTag> tags = new List<RecipeTag>();
            tags.Add(new RecipeTag(){Recipe = recipe,Tag = tag});
            tags.Add(new RecipeTag(){Recipe = recipe,Tag = tag});
           
            var ingredients = new List<Recipe>();
            ingredients.Add(new Recipe(){RecipeId = 32,RecipeAuthor = "Anis",RecipeName = "Tacos",RecipeTags = tags });
            ingredients.Add(new Recipe(){RecipeId = 329,RecipeAuthor = "Nour",RecipeName = "TacoBurrito",RecipeTags = tags });
            ingredients.Add(new Recipe(){RecipeId = 320,RecipeAuthor = "Beau",RecipeName = "Tacos",RecipeTags = tags });

            var result1 = new List<Recipe>();
            var result2 = new List<Recipe>();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.getAllRecipeByTags("Healthy");
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                result2 =  await context.Recipes.FromSqlRaw("$SELECT * FROM Recipes WHERE RecipeId IN (SELECT RecipeId FROM RecipeTags WHERE TagId = 3)").ToListAsync();
            }
            Assert.Equal(result1,result2);
        }
        public async Task Test4Async()
        {
            var recipe = new Recipe()
            {
                RecipeId = 4,
                RecipeName = "Cheese",
                RecipeAuthor = "Anis"
            };

            var result1 = new Recipe();
            var result2 = new Recipe();

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                var msr = new KitchenLogic(context);
                result1 = await msr.addNewRecipe(recipe);
            }

            await using(var context = new InTheKitchenDBContext(testOptions))
            {
                await context.Database.EnsureCreatedAsync();
                await context.Recipes.AddAsync(recipe);
                await context.SaveChangesAsync();
                result2 = context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == 4).Result;
            }
            Assert.Equal(result1,result2);
        }

    }
}
