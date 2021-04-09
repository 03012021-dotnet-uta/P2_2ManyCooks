using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Logic;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController
    {
        public readonly IReviewStepTagLogic iReviewStepTagLogic;

        public ReviewController(IReviewStepTagLogic iReviewStepTagLogic)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
        }

        [HttpGet("/byRecipeName/{recipe}")]
        public async Task<List<Review>> getAllTheReviewByRecipe (string recipe)
        {
            return await iReviewStepTagLogic.getReviewsByRecipeName(recipe);
        }

        [HttpGet("/byUser/{user}")]
        public async Task<List<Review>> getAllTheReviewByUser(string user)
        {
            return await iReviewStepTagLogic.getReviewByUser(user);
        }

    }
}
