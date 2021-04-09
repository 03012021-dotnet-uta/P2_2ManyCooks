using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Logic
{
    public interface IReviewStepTagLogic
    {
        Task<List<Tag>> geTags();
        Task<List<Ingredient>> getIngredients();
        Task<List<Review>> getReviewsByRecipeName(string recipeName);
        Task<List<Review>> getReviewByUser(string user);
        Task<Ingredient> getOneIngredientById(int id);
        Task<Ingredient> getOneIngredientByName(string name);
    }
}
