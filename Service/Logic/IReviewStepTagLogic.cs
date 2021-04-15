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
        Task<List<Review>> getReviewsByRecipeId(int id);

        /// <summary>
        /// Takes in Auth0 identifier sub
        /// Takes in the new review
        /// returns the new list of reviews
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="review"></param>
        /// <returns></returns>
        Task<List<Review>> addReview(string sub, Review review);
        Task<List<Review>> DeleteReview(int id);

        List<Review> GetAllReviews();
    }
}
