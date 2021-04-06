using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeAuthors = new HashSet<RecipeAuthor>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeTags = new HashSet<RecipeTag>();
            Reviews = new HashSet<Review>();
            Steps = new HashSet<Step>();
            UserViewHistories = new HashSet<UserViewHistory>();
        }

        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeAuthor { get; set; }
        public DateTime DateCreated { get; set; }
        public int? NumTimesPrepared { get; set; }
        public DateTime DateLastPrepared { get; set; }

        public virtual ICollection<RecipeAuthor> RecipeAuthors { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<UserViewHistory> UserViewHistories { get; set; }
    }
}
