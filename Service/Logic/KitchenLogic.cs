using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.LogicModels;
using Repository.Models;
using Repository.Repositories;

namespace Service.Logic
{
    public class KitchenLogic : ILogicKitchen
    {
        private InTheKitchenDBContext _context;
        private KitchenRepository _repo;
        public KitchenLogic(InTheKitchenDBContext _context, KitchenRepository _repo)
        {
            this._context = _context;
            this._repo = _repo;
        }

        // for test purpose 
        public KitchenLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }
        public List<Recipe> getAllRecipeByRecipeName(string recipeName)
        {
            if (!existRecipeName(recipeName))
            {
                return new List<Recipe>() { };

            }

            return _context.Recipes
                .Where(r => r.RecipeName == recipeName).ToList();
        }

        public async Task<Recipe> addNewRecipe(Recipe recipe)
        {

            recipe.RecipeId = getAllRecipe().Result.Count() + 1;
            recipe.DateCreated = DateTime.Now;
            recipe.DateLastPrepared = DateTime.Now;
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<List<Recipe>> getAllRecipeByTags(string tag)
        {

            //var tagg = _context.Tags.FirstOrDefault(t => t.TagName == tag);
            //int tagId = tagg.TagId;

            //if (!await existTag(tag))
            //{
            //    return new List<Recipe>(){};
            //}

            return await _context.Recipes.Include(r => r.RecipeTags).ThenInclude(r => r.Tag).AsQueryable()
                .ToListAsync();
            //return await _context.Recipes.FromSqlRaw($"SELECT * FROM Recipes WHERE RecipeId IN (SELECT RecipeId FROM RecipeTags WHERE TagId = {tagId})").ToListAsync();
        }

        public async Task<bool> existTag(string name)
        {
            List<Tag> tags = await _context.Tags.ToListAsync();

            return tags.Contains(await getOneTag(name));
        }

        public async Task<Tag> getOneTag(string tagName)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
            if (tag == null)
            {
                return null;
            }
            return tag;
        }
        public bool existRecipeName(string recipeName)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeName == recipeName);

            if (recipe == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Recipe>> getAllRecipe()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<ICollection<SentRecipe>> getAllSentRecipe()
        {
            ICollection<Recipe> rs = _repo.GetAllRecipes();
            return SentRecipe.MapMany(rs);
        }

        public SentRecipe GetRecipeById(int id)
        {
            var recipe = _repo.GetRecipeById(id);
            System.Console.WriteLine("recipe by id: " + recipe.RecipeName);
            return SentRecipe.GetFromRecipe(recipe);
        }

        public async Task<Recipe> getOneRecipeById(int id)
        {
            return await _context.Recipes.Where(r => r.RecipeId == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<SentRecipe>> DeleteRecipe(int id)
        {
            if (await _repo.DeleteRecipe(id))
            {
                return await getAllSentRecipe();
            }
            return null;
        }

        public async Task<SentRecipe> SaveRecipePrepare(HistoryModel historyModel)
        {
            Recipe recipe = await _repo.AddNewPrepare(historyModel.recipeId, historyModel.sub);
            if (recipe == null) return null;
            return SentRecipe.GetFromRecipe(recipe);
        }

        public async Task<SentRecipe> saveRecipe(SentRecipe sentRecipe, string sub)
        {
            Recipe dbRecipe = _repo.GetRecipeById(sentRecipe.RecipeId);
            if (dbRecipe == null)
            {
                dbRecipe = await _repo.SaveNewRecipe(sub);
            }
            Recipe otherRecipe = await _mapSentIntoRecipe(dbRecipe, sentRecipe);
            return SentRecipe.GetFromRecipe(otherRecipe);
        }

        private async Task<Recipe> _mapSentIntoRecipe(Recipe dbRecipe, SentRecipe sentRecipe)
        {
            dbRecipe.RecipeDescription = sentRecipe.RecipeDescription;
            dbRecipe.RecipeName = sentRecipe.RecipeName;
            dbRecipe.RecipeImage = sentRecipe.RecipeImage;
            var sentTags = sentRecipe.tags;
            List<Tag> dbTags = await _repo.GetAllTags();
            List<RecipeTag> dbRecipeTags = await _repo.GetAllRecipeTags();
            sentTags.ToList().ForEach(tag =>
            {
                var savedTag = dbTags.Where(t => t.TagId == tag.TagId).FirstOrDefault();
                if (savedTag == null)
                {
                    System.Console.WriteLine("tag not found: " + tag.TagName);
                    Tag dbTag = _repo.saveNewTag(tag);
                    _createRecipeTag(dbRecipe, dbTag);
                }
                else
                {
                    System.Console.WriteLine("tag found: " + tag.TagName);
                    var savedRecipeTag = dbRecipeTags.Where(rt => rt.TagId == savedTag.TagId && rt.RecipeId == dbRecipe.RecipeId).FirstOrDefault();
                    if (savedRecipeTag == null)
                    {
                        System.Console.WriteLine("added to recipe");
                        _createRecipeTag(dbRecipe, savedTag);
                    }
                }
            });
            dbRecipe.RecipeTags.ToList().ForEach(rt =>
            {
                if (!sentRecipe.tags.Any(t => t.TagName == rt.Tag.TagName && t.TagDescription == rt.Tag.TagDescription))
                {
                    System.Console.WriteLine("we gonna try not to remove: " + rt.Tag.TagName);
                    _repo.RemoveRecipeTag(rt);
                }
            });
            await _repo.SaveChanges();
            var sentIngredients = sentRecipe.ingredients;
            List<Ingredient> dbIngredients = await _repo.GetAllIngredients();
            List<RecipeIngredient> dbRecipeIng = await _repo.GetAllRecipeIngredients();
            sentIngredients.ToList().ForEach(ingredient =>
            {
                var savedIngredient = dbIngredients.Where(t => t.IngredientId == ingredient.IngredientId).FirstOrDefault();
                if (savedIngredient == null)
                {
                    System.Console.WriteLine("ingredient not found: " + ingredient.IngredientName);
                    Ingredient dbIngredient = _repo.saveNewIngredient(ingredient);
                    _createRecipeIngredient(dbRecipe, dbIngredient);
                }
                else
                {
                    System.Console.WriteLine("ingredient found: " + ingredient.IngredientName);
                    var savedRecipeIngredient = dbRecipeIng.Where(ri => ri.IngredientId == savedIngredient.IngredientId && ri.RecipeId == dbRecipe.RecipeId).FirstOrDefault();
                    if (savedRecipeIngredient == null)
                    {
                        System.Console.WriteLine("added to recipe");
                        _createRecipeIngredient(dbRecipe, savedIngredient);
                    }
                }
            });
            dbRecipe.RecipeIngredients.ToList().ForEach(rt =>
            {
                if (!sentRecipe.ingredients.Any(t => t.IngredientName == rt.Ingredient.IngredientName && t.IngredientDescription == rt.Ingredient.IngredientDescription))
                {
                    System.Console.WriteLine("we gonna try not to remove: " + rt.Ingredient.IngredientName);
                    _repo.RemoveRecipeIngredient(rt);
                }
            });
            await _repo.SaveChanges();
            // List<Step> dbSteps = await _repo.GetAllSteps();
            var dboldRecipeSteps = dbRecipe.Steps.ToList();
            var newSteps = sentRecipe.Steps.ToList();
            List<Step> savedSteps = new List<Step>();
            for (int i = 0; i < newSteps.Count; i++)
            {
                var dbStep = dboldRecipeSteps.Where(s => s.RecipeId == newSteps[i].RecipeId && s.RecipeStepNo == newSteps[i].RecipeStepNo && s.StepDescription == newSteps[i].StepDescription);
                if (dbStep == null)
                {
                    System.Console.WriteLine("step not found: " + newSteps[i].RecipeStepNo + " " + newSteps[i].StepDescription);
                    Step savedStep = _repo.SaveNewStep(newSteps[i]);
                    savedSteps.Add(savedStep);
                }
                else
                {
                    System.Console.WriteLine("step found: " + newSteps[i].RecipeStepNo + " " + newSteps[i].StepDescription);
                    savedSteps.Add(newSteps[i]);
                }
            }
            dboldRecipeSteps.ForEach(step =>
            {
                if (!savedSteps.Any(s => s.StepId == step.StepId))
                {
                    System.Console.WriteLine("old step deleted: " + step.RecipeStepNo + " " + step.StepDescription);
                    _repo.DeleteStep(step);
                }
            });
            dbRecipe.Steps = savedSteps;
            await _repo.SaveChanges();
            return dbRecipe;
        }

        private static void _createRecipeIngredient(Recipe recipe, Ingredient dbIngredient)
        {
            RecipeIngredient recipeIngredient = new RecipeIngredient();
            recipeIngredient.Ingredient = dbIngredient;
            recipeIngredient.IngredientId = dbIngredient.IngredientId;
            recipeIngredient.Recipe = recipe;
            recipeIngredient.RecipeId = recipe.RecipeId;
            recipe.RecipeIngredients.Add(recipeIngredient);
        }

        private static void _createRecipeTag(Recipe recipe, Tag dbTag)
        {
            RecipeTag recipeTag = new RecipeTag();
            recipeTag.Tag = dbTag;
            recipeTag.TagId = dbTag.TagId;
            recipeTag.Recipe = recipe;
            recipeTag.RecipeId = recipe.RecipeId;
            recipe.RecipeTags.Add(recipeTag);
        }
    }
}