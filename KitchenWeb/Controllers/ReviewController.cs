using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Interfaces;
using Service.Logic;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        public readonly IReviewStepTagLogic iReviewStepTagLogic;
        public readonly IAuthenticator _authenticator;

        public ReviewController(IReviewStepTagLogic iReviewStepTagLogic, IAuthenticator _authenticator)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
            this._authenticator = _authenticator;
        }

        [HttpGet("/byRecipeName/{recipe}")]
        public async Task<List<Review>> getAllTheReviewByRecipe(string recipe)
        {
            return await iReviewStepTagLogic.getReviewsByRecipeName(recipe);
        }


        [HttpGet("recipe/{id}")]
        public async Task<List<Review>> getAllTheReviewByRecipeId(int id)
        {
            return await iReviewStepTagLogic.getReviewsByRecipeId(id);
        }


        [HttpPost]
        [Authorize]
        public async Task<List<Review>> AddNewReview([FromBody] Review review)
        {
            var tok = ControllerHelper.GetTokenFromRequest(this.Request);
            var dic = await _authenticator.GetUserAuth0Dictionary(tok);
            return await iReviewStepTagLogic.addReview(dic["sub"], review);
        }

        [HttpGet("/byUser/{user}")]
        public async Task<List<Review>> getAllTheReviewByUser(string user)
        {
            return await iReviewStepTagLogic.getReviewByUser(user);
        }

        [HttpDelete("{id}")]
        [Authorize("update:website")]
        public async Task<List<Review>> DeleteReview(int id)
        {
            return await iReviewStepTagLogic.DeleteReview(id);
        }

    }
}
