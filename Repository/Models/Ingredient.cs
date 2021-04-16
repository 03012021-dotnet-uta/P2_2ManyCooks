using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable

namespace Repository.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public string IngredientImage { get; set; }
        public string? ThirdPartyApiId { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        //public static implicit operator ValueTask<object>(Ingredient v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
