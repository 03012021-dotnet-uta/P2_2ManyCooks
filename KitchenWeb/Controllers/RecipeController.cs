﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Helpers;
using Service.Logic;
using Models.LogicModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
            System.Console.WriteLine("getting all recipes");
            return await iLogicKitchen.getAllSentRecipe();
        }


        [HttpGet("good/{id}")]
        public ActionResult<SentRecipe> getGoodById(int id)
        {
            System.Console.WriteLine("getting recipe by id: " + id);
            return iLogicKitchen.GetRecipeById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ICollection<SentRecipe>> DeleteRecipe(int id)
        {
            return await iLogicKitchen.DeleteRecipe(id);
        }

        [HttpGet("recipeById/{id}")]
        public async Task<Recipe> getOne(int id)
        {
            return await iLogicKitchen.getOneRecipeById(id);
        }

        [HttpGet("/recipeName/{recipeName}")]
        public List<Recipe> getThemByRecipeName(string recipeName)
        {
            return iLogicKitchen.getAllRecipeByRecipeName(recipeName);
        }

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


        [HttpPost("history")]
        [Authorize]
        public async Task<ActionResult<SentRecipe>> savePrepare([FromBody] HistoryModel historyModel)
        {
            // System.Console.WriteLine(body);
            System.Console.WriteLine("id: " + historyModel.recipeId + " sub: " + historyModel.sub);
            SentRecipe recipe = await this.iLogicKitchen.SaveRecipePrepare(historyModel);
            return recipe;
        }
    }
}
