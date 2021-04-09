using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Service.Logic
{
    public class ReviewStepTagLogic : IReviewStepTagLogic
    {
        private InTheKitchenDBContext _context;
        public ReviewStepTagLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
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
                    Where(r =>r.Recipe.RecipeName == recipeName).ToListAsync();
            }

            return new List<Review>(){};
        }

        public async Task<List<Review>> getReviewByUser(string user)
        {
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.Firstname == user || u.Lastname == user );
            if (user1 != null)
            {
                return await _context.Reviews.Include(r => r.User).
                    Where(r =>r.User.Firstname == user || r.User.Lastname == user).ToListAsync();
            }

            return new List<Review>(){};
        }
    }
}
