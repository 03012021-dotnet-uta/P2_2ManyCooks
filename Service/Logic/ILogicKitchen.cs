using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.LogicModels;
using Repository.Models;

namespace Service.Logic
{
    public interface ILogicKitchen
    {
        Task<List<Recipe>> getAllRecipeByTags(string tag);
        List<Recipe> getAllRecipeByRecipeName(string recipeName);
        Task<List<Recipe>> getAllRecipe();
        Task<ICollection<SentRecipe>> getAllSentRecipe();
        Task<Recipe> addNewRecipe(Recipe recipe);
        bool existRecipeName(string recipeName);
        Task<Tag> getOneTag(string tagName);
        Task<bool> existTag(string name);
        SentRecipe GetRecipeById(int id);
        Task<Recipe> getOneRecipeById(int id);
        Task<ICollection<SentRecipe>> DeleteRecipe(int id);
    }
}
