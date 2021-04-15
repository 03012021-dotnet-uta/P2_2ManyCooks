using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KitchenWeb.Helpers;
using Microsoft.AspNetCore.Http;
using Models.LogicModels;
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
                IngredientName = "Cheese",
                IngredientDescription = "svsvsvs",
                IngredientImage = "Some Image",
                RecipeIngredients = new List<RecipeIngredient>(),
                ThirdPartyApiId = "Google",
                
            };
            var expectedid = 3;
            var expected = "Cheese";
            var expected1 = "svsvsvs";
            var expected2 = "Some Image";
            var expected3 = new List<RecipeIngredient>();
            var expected4 = "Google";
            var result = sut.IngredientName;
            var result2 = sut.IngredientDescription;
            var result3 = sut.IngredientImage;
            var result4 = sut.RecipeIngredients;
            var result5= sut.ThirdPartyApiId;
            var result6 = sut.IngredientId;
            Assert.Equal(expectedid,result6);
            Assert.Equal(expected,result);
            Assert.Equal(expected1,result2);
            Assert.Equal(expected2,result3);
            Assert.Equal(expected3,result4);
            Assert.Equal(expected4,result5);
        }
        [Fact]
        public void TestRecipe()
        {
            var sut = new Recipe()
            {
                RecipeId = 3,
                RecipeAuthor = "Anis",
                RecipeDescription = "svsvsvs",
                RecipeIngredients = new List<RecipeIngredient>(),
                RecipeImage = "Some Image",
                RecipeName = "Cheese",
                RecipeTags = new List<RecipeTag>(),
                RecipeAuthors = new List<RecipeAuthor>(),
                Reviews = new List<Review>()
            };
            var expected1 = 3;
            var expected2 = "Anis";
            var expected3 = "svsvsvs";
            var expected4 = new List<RecipeIngredient>();
            var expected5 = "Some Image" ;
            var expected6 = "Cheese";
            var expected7 = new List<RecipeTag>();
            var expected8 = new List<RecipeAuthor>();
            var expected9 = new List<Review>();

            var result1 = sut.RecipeId;
            var result2 = sut.RecipeAuthor;
            var result3 = sut.RecipeDescription;
            var result4 = sut.RecipeIngredients;
            var result5 = sut.RecipeImage;
            var result6 = sut.RecipeName;
            var result7 = sut.RecipeTags;
            var result8 = sut.RecipeAuthors;
            var result9 = sut.Reviews;
        


         
            Assert.Equal(expected1,result1);
            Assert.Equal(expected2,result2);
            Assert.Equal(expected3,result3);
            Assert.Equal(expected4,result4);
            Assert.Equal(expected5,result5);
            Assert.Equal(expected6,result6);
            Assert.Equal(expected7,result7);
            Assert.Equal(expected8,result8);
            Assert.Equal(expected9,result9);


        }
        [Fact]
        public void TestRecipeAuthor()
        {
            var sut = new RecipeAuthor()
            {
                RecipeId = 4,
                UserId = 56,
                Recipe = new Recipe(){RecipeId = 4,RecipeName = "Tacos"},
                User = new User(){UserId = 56,Username = "Anis"}
                
            };

            var expected = "Anis";
            var expected2 = 56;
            var expected3 = 4;
            var expected4 = "Tacos";

            var result = sut.User.Username;
            var result2 = sut.UserId;
            var result3 = sut.RecipeId;
            var result4 = sut.Recipe.RecipeName;
            Assert.Equal(expected,result);
            Assert.Equal(expected2,result2);
            Assert.Equal(expected3,result3);
            Assert.Equal(expected4,result4);

        }
        [Fact]
        public void TestRecipeIngredient()
        {
            var sut = new RecipeIngredient()
            {
                RecipeId = 54,
                IngredientId = 4,
                Ingredient = new Ingredient(){IngredientId = 4,IngredientName = "Cheese"},
                Recipe = new Recipe(){RecipeId = 54,RecipeName = "Tacos"}
                
                
            };

            var expected = "Tacos";
            var expected2 = 54;
            var expected3 = 4;
            var expected4 = "Cheese";
            var result = sut.Recipe.RecipeName;
            var result2 = sut.RecipeId;
            var result3 = sut.IngredientId;
            var result4 = sut.Ingredient.IngredientName;

            Assert.Equal(expected,result);
            Assert.Equal(expected2,result2);
            Assert.Equal(expected3,result3);
            Assert.Equal(expected4,result4);

        }
        [Fact]
        public void TestRecipeTag()
        {
            var sut = new RecipeTag()
            {
                RecipeId = 2,
                TagId = 3,
                Tag = new Tag(){TagId = 3,TagName = "Sandwich"},
                Recipe = new Recipe(){RecipeId = 2,RecipeName = "Cheese"}
            };

            var expected = "Sandwich";
            var expected2 = 2;
            var expected3 = 3;
            var expected4 = "Cheese"; 
            var result = sut.Tag.TagName;
            var result2 = sut.RecipeId;
            var result3 = sut.TagId;
            var result4 = sut.Recipe.RecipeName;
            Assert.Equal(expected,result);
            Assert.Equal(expected2,result2);
            Assert.Equal(expected3,result3);
            Assert.Equal(expected4,result4);

        }
        [Fact]
        public void TestReview()
        {
            var sut = new Review()
            {
                ReviewId = 3,

                ReviewDescription = "Message",
                ReviewRating = 5,
                ReviewDate = DateTime.Today,
                RecipeId = 45,
                UserId = 55,

            };
            var expected1 = 3 ;
            var expected2 = "Message";
            var expected3 = 5;
            var expected4 =DateTime.Today;
            var expected5 = 45;
            var expected6 = 55;

            var result1 = sut.ReviewId;
            var result2 = sut.ReviewDescription;
            var result3 = sut.ReviewRating;
            var result4 = sut.ReviewDate;
            var result5 = sut.RecipeId;
            var result6 = sut.UserId;
            Assert.Equal(expected1,result1);
            Assert.Equal(expected2,result2);
            Assert.Equal(expected3,result3);
            Assert.Equal(expected4,result4);
            Assert.Equal(expected5,result5);
            Assert.Equal(expected6,result6);
        }
        [Fact]
        public void TestStep()
        {
            var sut = new Step()
            {
                StepId = 3,
                StepDescription = "sfvsfxbdfxb",
                RecipeStepNo = 4,
                StepImage = "Hello",
                RecipeId = 4,
                Recipe = new Recipe(){RecipeId = 5,RecipeName = "Cheese"}

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
                TagDescription = "adcadcadcadc",
                TagName = "Cheese",
                RecipeTags = new List<RecipeTag>()

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
                Firstname = "Anis",
                Lastname = "Medini",
                PasswordSalt = "SomePassword",
                PermissionId = 1,
                Permission = new UserPermission(),
                PasswordHash = "SomeHash",
                Email = "Anis@gmail.com",
                DateCreated = DateTime.Today,
                DateLastAccessed = DateTime.Today,
                Auth0 = "SomeAuth",
                ImageUrl = "SomeImage",
                RecipeAuthors = new List<RecipeAuthor>(),
                Reviews = new List<Review>(),
                UserSearchHistories = new List<UserSearchHistory>(),
                UserViewHistories = new List<UserViewHistory>(),

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
                HistoryId = 47,
                SearchString = "Anis",
                UserId = 4,
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
                ViewDate = DateTime.Today,
                RecipeId = 34,
                Recipe = new Recipe(),
                User = new User(),
                UserId = 32,
            };

            var expected = DateTime.Today;
            var result = sut.ViewDate;
            Assert.Equal(expected,result);

        }

        [Fact]
        public void TestAUthModel()
        {
            var auth = new AuthModel()
            {
                Username = "Anis1011",
                Email = "Anis1011@gmail.com",
                FirstName = "Anis",
                LastName = "Medini",
                ProfileImage = "SomeImage",
                Password = "Password",
                Sub = "Some sub"
            };
            var expected = "Anis1011";
            var result = auth.Username;
            Assert.Equal(result,expected);
        }
        [Fact]
        public void TestMapperUser()
        {
            var auth = new AuthModel()
            {
                Username = "Anis1011",
                Email = "Anis1011@gmail.com",
                FirstName = "Anis",
                LastName = "Medini",
                ProfileImage = "SomeImage",
                Password = "Password",
                Sub = "Some sub"
            };

            User user = new User()
            {
                Username = auth.Username,
                Firstname = auth.FirstName,
                Lastname = auth.LastName,
                Email = auth.Email,
                PasswordHash = "",
                PasswordSalt = "",
                DateLastAccessed = DateTime.Now,
                ImageUrl = auth.ProfileImage,
                Auth0 = auth.Sub,
            };
            var expected = "Anis1011";
            var result = user.Username;
            Assert.Equal(result,expected);
        }

        [Fact]
        public void TestSendRecipe()
        {
            var recipeSent = new SentRecipe()
            {
                RecipeId = 4,
                RecipeName = "Cheese",
                RecipeDescription = "Something",
                RecipeImage = "Some Image",
                RecipeAuthor = "Anis",
                DateCreated = DateTime.Today,
                NumTimesPrepared = 5,
                DateLastPrepared = DateTime.Today,
                ingredients = new List<Ingredient>(),
                tags = new List<Tag>(),
                Steps = new List<Step>(),
                Reviews = new List<Review>(),

            };
            var expected = "Anis";
            var result = recipeSent.RecipeAuthor;
            Assert.Equal(expected,result);
        }

        [Fact]
        public void TestGetFromRecipe()
        {
            var tagList = new List<Tag>();
            var recipe = new Recipe()
            {
                RecipeId = 3,
                RecipeAuthor = "Anis",
                RecipeDescription = "svsvsvs",
                RecipeIngredients = new List<RecipeIngredient>(),
                RecipeImage = "Some Image",
                RecipeName = "Cheese",
                RecipeTags = new List<RecipeTag>(),
                RecipeAuthors = new List<RecipeAuthor>(),
                Reviews = new List<Review>() 
            };

            foreach (var RecipeTag in recipe.RecipeTags)
            {
                tagList.Add(RecipeTag.Tag);
                tagList.Add(new Tag(){TagId = 4});
            }
            var tagIng = new List<Ingredient>();
            foreach (var RecipeTag in recipe.RecipeIngredients)
            {
                tagIng.Add(RecipeTag.Ingredient);
            }
            Assert.Empty(tagList);

        }
    }
}
