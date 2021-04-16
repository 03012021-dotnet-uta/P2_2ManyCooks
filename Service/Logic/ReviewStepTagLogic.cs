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
            return await _context.Tags.Include(t => t.RecipeTags).ToListAsync();
        }

        public async Task<List<Ingredient>> getIngredients()
        {
            return await _context.Ingredients.Include(t => t.RecipeIngredients).ToListAsync();
        }

        public async Task<List<Review>> getReviewsByRecipeName(string recipeName)
        {
            return await _context.Reviews.Include(r => r.Recipe).Where(r => r.Recipe.RecipeName == recipeName)
                .ToListAsync();
        }

        public async Task<List<Review>> getReviewByUser(string user)
        {
            return await _context.Reviews.Include(r => r.User).
                    Where(r => r.User.Firstname == user || r.User.Lastname == user).ToListAsync();
        }

        public async Task<Ingredient> getOneIngredientById(int id)
        {
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
            if (await _repo.DeleteReview(id))
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
