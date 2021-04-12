using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Helpers;
using Service.Logic;
using Models.LogicModels;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController
    {
        public readonly ILogicKitchen iLogicKitchen;

        public RecipeController(ILogicKitchen iLogicKitchen)
        {
            this.iLogicKitchen = iLogicKitchen;
        }

        [HttpPost]
        public async Task<Recipe> addNewRec([FromBody] Recipe recipe)
        {
            await iLogicKitchen.addNewRecipe(recipe);

            return recipe;

        }

        [HttpGet]
        public async Task<List<Recipe>> getThemAll()
        {
            return await iLogicKitchen.getAllRecipe();
        }


        [HttpGet("good")]
        public async Task<ICollection<SentRecipe>> getThemAllGood()
        {
            return await iLogicKitchen.getAllSentRecipe();
        }


        [HttpGet("good/{id}")]
        public ActionResult<SentRecipe> getGoodById(int id)
        {
            return iLogicKitchen.GetRecipeById(id);
        }

        [HttpGet("/recipeName/{recipeName}")]
        public List<Recipe> getThemByRecipeName(string recipeName)
        {
            return iLogicKitchen.getAllRecipeByRecipeName(recipeName);
        }

        // not working properly 
        [HttpGet("/tag/{tag}")]
        public async Task<List<Recipe>> getThemByTag(string tag)
        {
            return await iLogicKitchen.getAllRecipeByTags(tag);
        }

        [HttpGet("/api/{search}")]
        public async Task<RecipeModel> getInfo(string search)
        {
            return await RecipeProcessor.LoadRecipe(search);
        }
    }
}
