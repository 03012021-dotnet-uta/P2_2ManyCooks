using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers;
using Repository.Models;

namespace Repository.Repositories
{
    public class KitchenRepository
    {
        private InTheKitchenDBContext _context;

        public KitchenRepository(InTheKitchenDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Checks if user exists in our DB
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if user exists</returns>
        public bool DoesUserExist(string Auth0)
        {
            return _context.Users.Any(u => u.Auth0 == Auth0);
        }

        public bool UpdateUserAuth0Data(User user)
        {
            // todo: talk about what we want to save
            // * {"sub":"auth0|606e2aeaa32e9700697566ba",
            // * "nickname":"noureldinashraf6",
            // * "name":"noureldinashraf6@gmail.com",
            // * "picture":"https://s.gravatar.com/avatar/162c31c37ca4c96d5bf9e43b9e2bd5a0?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fno.png",
            // * "updated_at":"2021-04-08T13:21:23.390Z",
            // * "email":"noureldinashraf6@gmail.com",
            // * "email_verified":false}
            var dbUser = _context.Users.Where(u => u.Auth0 == user.Auth0).FirstOrDefault();

            dbUser.Email = user.Email;
            dbUser.ImageUrl = user.ImageUrl;
            // dbUser.DateLastAccessed = TimeManager.GetTimeNow();
            dbUser.DateLastAccessed = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// only updates the following:
        /// dbuser.Email = inUser.Email;
        /// dbuser.Firstname = inUser.Firstname;
        /// dbuser.Lastname = inUser.Lastname;
        /// dbuser.ImageUrl = inUser.ImageUrl;
        /// dbuser.Username = inUser.Username;
        /// </summary>
        /// <param name="inUser"></param>
        /// <param name="OutUser"></param>
        /// <returns></returns>
        public bool UpdateUserPrimaryData(User inUser, out User OutUser)
        {
            var dbuser = _context.Users.Where(u => u.Auth0 == inUser.Auth0).FirstOrDefault();
            if (dbuser == null)
            {
                OutUser = null;
                return false;
            }
            // todo: if we want to save more data, change here as well as in save new user here
            dbuser.Email = inUser.Email;
            dbuser.Firstname = inUser.Firstname;
            dbuser.Lastname = inUser.Lastname;
            dbuser.ImageUrl = inUser.ImageUrl;
            dbuser.Username = inUser.Username;

            OutUser = dbuser;

            return _context.SaveChanges() > 0;
        }

        public Task<List<Review>> AddNewReview(string sub, Review review)
        {
            var dbUser = _context.Users.Where(u => u.Auth0 == sub).FirstOrDefault();
            if (_context.Reviews.Any(r => r.UserId == dbUser.UserId && r.RecipeId == review.RecipeId))
            {
                return null;
            }
            review.ReviewDate = DateTime.Now;
            review.User = dbUser;
            review.UserId = dbUser.UserId;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return _context.Reviews.Where(r => r.RecipeId == review.RecipeId).Include(r => r.User).ToListAsync();
        }

        public Task<List<Review>> GetReviewsByRecipeId(int recipeId)
        {
            return _context.Reviews.Where(r => r.RecipeId == recipeId)
            .Include(r => r.User)
            .ToListAsync();
        }

        public User GetUserDataById(int id)
        {
            return _context.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public User GetUserDataBySub(string sub)
        {
            return _context.Users.Where(u => u.Auth0 == sub).FirstOrDefault();
        }

        /// <summary>
        /// Save a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newUser"></param>
        /// <returns>the new saved user</returns>
        public bool SaveNewUser(User user, out User newUser)
        {
            _context.Users.Add(user);
            bool success = _context.SaveChanges() > 0;

            newUser = _context.Users.Where(u => u.Auth0 == user.Auth0).FirstOrDefault();
            return success;
        }

        public ICollection<Recipe> GetAllRecipes()
        {
            return _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
            .Include(r => r.Steps)
            .ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return _context.Recipes.Where(r => r.RecipeId == id)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
            .Include(r => r.Steps)
            .Include(r => r.Reviews)
            .FirstOrDefault();
        }
    }
}