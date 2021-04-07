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
            return await _context.Tags.FromSqlRaw("Select * from Tags").ToListAsync();
        }

        public async Task<List<Ingredient>> getIngredients()
        {
            return await _context.Ingredients.FromSqlRaw("Select * from Ingredients").ToListAsync();
        }
    }
}
