using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var dbUser = _context.Users.FirstOrDefault(u => u.Auth0 == user.Auth0);

            dbUser.Email = user.Email;
            dbUser.ImageUrl = user.ImageUrl;
            // dbUser.DateLastAccessed = TimeManager.GetTimeNow();
            dbUser.DateLastAccessed = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
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
            var dbUser = _context.Users.FirstOrDefault(u => u.Auth0 == sub);
            if (_context.Reviews.Any(r => r.UserId == dbUser.UserId && r.RecipeId == review.RecipeId))
            {
                return Task.FromResult<List<Review>>(null);
            }
            review.ReviewDate = DateTime.Now;
            review.User = dbUser;
            review.UserId = dbUser.UserId;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return _context.Reviews.Where(r => r.RecipeId == review.RecipeId).Include(r => r.User).ToListAsync();
        }

        public List<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public Task<List<Review>> GetReviewsByRecipeId(int recipeId)
        {
            return _context.Reviews.Where(r => r.RecipeId == recipeId)
            .Include(r => r.User)
            .ToListAsync();
        }

        public User GetUserDataById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public User GetUserDataBySub(string sub)
        {
            return _context.Users.FirstOrDefault(u => u.Auth0 == sub);
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

            newUser = _context.Users.FirstOrDefault(u => u.Auth0 == user.Auth0);
            return success;
        }

        public async Task<Recipe> SaveNewRecipe(string sub)
        {
            var user = await _context.Users.Where(u => u.Auth0 == sub).FirstOrDefaultAsync();
            Recipe r = new Recipe();
            r.DateCreated = DateTime.Now;
            r.DateLastPrepared = DateTime.Now;
            r.NumTimesPrepared = 0;
            r.RecipeDescription = "";
            r.RecipeName = "";
            r.RecipeAuthor = user.Firstname + " " + user.Lastname;
            var author = new RecipeAuthor();
            author.User = user;
            r.RecipeAuthors.Add(author);
            var x = await _context.Recipes.AddAsync(r);
            await _context.SaveChangesAsync();
            System.Console.WriteLine("is id here? " + r.RecipeId);
            return r;
        }

        public Task<List<Tag>> GetAllTags()
        {
            return _context.Tags.Include(t => t.RecipeTags).ThenInclude(rt => rt.Recipe).ToListAsync();
        }

        public async Task<List<RecipeTag>> GetAllRecipeTags()
        {
            return await _context.RecipeTags
            .Include(rt => rt.Recipe)
            .Include(rt => rt.Tag)
            .ToListAsync();
        }

        public ICollection<Recipe> GetAllRecipes()
        {
            return _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
            .Include(r => r.Steps)
            .Include(r => r.Reviews)
            .ToList();
        }

        public Tag saveNewTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return _context.Tags.Where(t => tag.TagName == t.TagName).FirstOrDefault();
        }

        public async Task<List<RecipeIngredient>> GetAllRecipeIngredients()
        {
            return await _context.RecipeIngredients
            .Include(ri => ri.Ingredient)
            .Include(ri => ri.Recipe)
            .ToListAsync();
        }

        public void RemoveRecipeTag(RecipeTag rt)
        {
            _context.RecipeTags.Remove(rt);
            _context.SaveChanges();
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients.Include(i => i.RecipeIngredients).ToListAsync();
        }

        public Ingredient saveNewIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            return _context.Ingredients.Where(t => ingredient.IngredientName == t.IngredientName).FirstOrDefault();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
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

        public async Task<List<Step>> GetAllSteps()
        {
            return await _context.Steps.ToListAsync();
        }

        public async Task<Recipe> AddNewPrepare(int recipeId, string sub)
        {

            var dbrecipe = await _context.Recipes.Where(r => r.RecipeId == recipeId).FirstOrDefaultAsync();
            if (dbrecipe == null) return null;

            var dbuser = await _context.Users.Where(u => u.Auth0 == sub)
            .Include(u => u.UserSearchHistories)
            .FirstOrDefaultAsync();
            if (dbuser == null) return null;

            if (await _context.UserViewHistories.AnyAsync(h => h.RecipeId == recipeId && dbuser.Auth0 == sub))
            {
                System.Console.WriteLine("user viewed before");
                return null;
            }

            dbrecipe.NumTimesPrepared++;
            if (_context.SaveChanges() <= 0)
            {
                System.Console.WriteLine("couldn't save prepare");
                return null;
            }

            var history = new UserViewHistory();
            history.ViewDate = DateTime.Now;
            history.RecipeId = dbrecipe.RecipeId;
            history.UserId = dbuser.UserId;
            _context.UserViewHistories.Add(history);
            if (_context.SaveChanges() <= 0)
            {
                System.Console.WriteLine("couldn't save view history");
                return null;
            }
            return dbrecipe;
        }

        public void RemoveRecipeIngredient(RecipeIngredient rt)
        {
            _context.RecipeIngredients.Remove(rt);
            _context.SaveChanges();
        }

        public void DeleteStep(Step step)
        {
            step.RecipeId = null;
            step.Recipe = null;
            _context.SaveChanges();
            _context.Steps.Remove(step);
            _context.SaveChanges();
        }

        public Step SaveNewStep(Step step)
        {
            _context.Steps.Add(step);
            _context.SaveChanges();
            return _context.Steps.Where(t => t.RecipeStepNo == step.RecipeStepNo && t.RecipeId == step.RecipeId).FirstOrDefault();
        }

        public async Task<bool> DeleteUser(string sub)
        {
            var dbuser = await _context.Users.Where(u => u.Auth0 == sub)
            .Include(u => u.Permission)
            .Include(u => u.RecipeAuthors)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync();
            if (dbuser == null) return false;

            dbuser.Reviews = null;
            dbuser.Permission = null;
            dbuser.PermissionId = null;
            dbuser.RecipeAuthors = null;

            if (_context.SaveChanges() > 0)
            {
                _context.Users.Remove(dbuser);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRecipe(int id)
        {
            var dbRecipe = await _context.Recipes.Where(u => u.RecipeId == id).FirstOrDefaultAsync();
            if (dbRecipe == null) return false;

            _context.Recipes.Remove(dbRecipe);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteReview(int id)
        {
            System.Console.WriteLine("in repo review deleting");
            var dbReview = await _context.Reviews.Where(u => u.ReviewId == id).FirstOrDefaultAsync();
            if (dbReview == null) return false;

            dbReview.UserId = null;
            dbReview.RecipeId = null;
            if (_context.SaveChanges() > 0)
            {
                _context.Reviews.Remove(dbReview);
                return _context.SaveChanges() > 0;
            }
            System.Console.WriteLine("false");
            return false;
        }
    }
}