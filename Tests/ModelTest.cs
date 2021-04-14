using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;
using Xunit;

namespace Tests
{
   public class ModelTest
    {

        [Fact]
        public void TestIngredient()
        {
            var sut = new Ingredient()
            {
                IngredientId = 3,
                IngredientName = "Cheese"
            };

            var expected = "Cheese";
            var result = sut.IngredientName;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestRecipe()
        {
            var sut = new Recipe()
            {
                RecipeId = 3,
                RecipeName = "Tacos"
            };

            var expected = "Tacos";
            var result = sut.RecipeName;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestRecipeAuthor()
        {
            var sut = new RecipeAuthor()
            {
                Recipe = new Recipe(){RecipeId = 4,RecipeName = "Tacos"},
                User = new User(){UserId = 56,Username = "Anis"}
            };

            var expected = "Anis";
            var result = sut.User.Username;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestRecipeIngredient()
        {
            var sut = new RecipeIngredient()
            {
                Ingredient = new Ingredient(){IngredientId = 4,IngredientName = "Cheese"},
                Recipe = new Recipe(){RecipeId = 54,RecipeName = "Tacos"}
                
            };

            var expected = "Tacos";
            var result = sut.Recipe.RecipeName;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestRecipeTag()
        {
            var sut = new RecipeTag()
            {
                Tag = new Tag(){TagId = 3,TagName = "Sandwich"},
                Recipe = new Recipe(){RecipeId = 2,RecipeName = "Cheese"}
            };

            var expected = "Sandwich";
            var result = sut.Tag.TagName;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestReview()
        {
            var sut = new Review()
            {
                ReviewId = 3,
                ReviewDate = DateTime.Now
            };

            var expected = DateTime.Now;
            var result = sut.ReviewDate;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestStep()
        {
            var sut = new Step()
            {
                StepId = 3,
                StepDescription = "sfvsfxbdfxb"
            };

            var expected = "sfvsfxbdfxb";
            var result = sut.StepDescription;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestTag()
        {
            var sut = new Tag()
            {
                TagId = 3,
                TagDescription = "adcadcadcadc"
            };

            var expected = "adcadcadcadc";
            var result = sut.TagDescription;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestUser()
        {
            var sut = new User()
            {
                UserId = 3,
                Username = "Anis1011",
                Firstname = "Anis"
            };

            var expected = "Anis";
            var result = sut.Firstname;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestUserPermission()
        {
            var sut = new UserPermission()
            {
                PermissionId = 3,
                PermissionName = "Admin"
            };

            var expected = "Admin";
            var result = sut.PermissionName;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestUserSearchHistory()
        {
            var sut = new UserSearchHistory()
            {
                User = new User(){UserId = 4,Lastname = "Medini"},
                SearchDate = DateTime.Today
            };

            var expected = DateTime.Today;
            var result = sut.SearchDate;
            Assert.Equal(expected,result);

        }
        [Fact]
        public void TestViewHistory()
        {
            var sut = new UserViewHistory()
            {
                HistoryId = 3,
                ViewDate = DateTime.Today
            };

            var expected = DateTime.Today;
            var result = sut.ViewDate;
            Assert.Equal(expected,result);

        }
      
    }

}
