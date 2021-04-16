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
    public class IngredientController {
        public readonly IReviewStepTagLogic iReviewStepTagLogic;

        public IngredientController(IReviewStepTagLogic iReviewStepTagLogic)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
        }


        [HttpGet]
        public async Task<List<Ingredient>> GetIngredients()
        {
            return await iReviewStepTagLogic.getIngredients();
        }

        [HttpGet("/byIngredientName/{name}")]
        public async Task<Ingredient> getIngredByName(string name)
        {
            return await iReviewStepTagLogic.getOneIngredientByName(name);
        }
        [HttpGet("/byIngredientId/{id}")]
        public async Task<Ingredient> getIngredById(int id)
        {
            return await iReviewStepTagLogic.getOneIngredientById(id);
        }
       
    }
}
