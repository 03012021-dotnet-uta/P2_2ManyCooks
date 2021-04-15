using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories;

namespace Service.Logic
{
    public class ReviewStepTagLogic : IReviewStepTagLogic
    {
        private readonly InTheKitchenDBContext _context;
        private readonly KitchenRepository _repo;
        public ReviewStepTagLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }


        public ReviewStepTagLogic(InTheKitchenDBContext _context, KitchenRepository _repo)
        {
            this._context = _context;
            this._repo = _repo;
        }

        public async Task<List<Tag>> geTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<List<Ingredient>> getIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<List<Review>> getReviewsByRecipeName(string recipeName)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeName == recipeName);
            if (recipe != null)
            {
                return await _context.Reviews.Include(r => r.Recipe).
                    Where(r => r.Recipe.RecipeName == recipeName).ToListAsync();
            }

            return new List<Review>() { };
        }

        public async Task<List<Review>> getReviewByUser(string user)
        {
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.Firstname == user || u.Lastname == user);
            if (user1 != null)
            {
                return await _context.Reviews.Include(r => r.User).
                    Where(r => r.User.Firstname == user || r.User.Lastname == user).ToListAsync();
            }

            return new List<Review>() { };
        }

        public async Task<Ingredient> getOneIngredientById(int id)
        {
            if (await _context.Ingredients.FindAsync(id) == null)
            {
                throw new Exception("No Ingredient Matching this ID: " + id);
            }

            return await _context.Ingredients.FindAsync(id);
        }

        public async Task<Ingredient> getOneIngredientByName(string name)
        {

            return await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientName == name);
        }

        public Task<List<Review>> getReviewsByRecipeId(int recipeId)
        {
            return _repo.GetReviewsByRecipeId(recipeId);
        }

        public Task<List<Review>> addReview(string sub, Review review)
        {
            return _repo.AddNewReview(sub, review);
        }

        public async Task<List<Review>> DeleteReview(int id)
        {
            if (await _repo.DeleteRecipe(id))
            {
                return GetAllReviews();
            }
            return null;
        }

        public List<Review> GetAllReviews()
        {
            return _repo.GetAllReviews();
        }
    }
}
