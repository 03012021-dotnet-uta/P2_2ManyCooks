using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Models;

namespace Models.LogicModels
{
    public class SentRecipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeAuthor { get; set; }
        public DateTime DateCreated { get; set; }
        public int? NumTimesPrepared { get; set; }
        public DateTime DateLastPrepared { get; set; }
        public virtual ICollection<Ingredient> ingredients { get; set; }
        public virtual ICollection<Tag> tags { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public static SentRecipe GetFromRecipe(Recipe recipe)
        {
            var tagList = new List<Tag>();
            recipe.RecipeTags.ToList().ForEach(rtag =>
            {
                tagList.Add(rtag.Tag);
            });
            var ingList = new List<Ingredient>();
            recipe.RecipeIngredients.ToList().ForEach(rIng =>
            {
                ingList.Add(rIng.Ingredient);
            });
            return new SentRecipe()
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                RecipeDescription = recipe.RecipeDescription,
                RecipeImage = recipe.RecipeImage,
                RecipeAuthor = recipe.RecipeAuthor,
                DateCreated = recipe.DateCreated,
                DateLastPrepared = recipe.DateLastPrepared,
                NumTimesPrepared = recipe.NumTimesPrepared,
                Steps = recipe.Steps,
                tags = tagList,
                ingredients = ingList,
                Reviews = recipe.Reviews
            };
        }

        public static ICollection<SentRecipe> MapMany(ICollection<Recipe> recipes)
        {
            var srecipes = new List<SentRecipe>();
            recipes.ToList().ForEach(r =>
            {
                srecipes.Add(SentRecipe.GetFromRecipe(r));
            });
            return srecipes;
        }
    }
}