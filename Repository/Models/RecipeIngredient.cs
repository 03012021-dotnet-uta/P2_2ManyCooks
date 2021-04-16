using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Repository.Models
{
    public partial class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int quantity_grams { get; set; }

        [JsonIgnore]
        public virtual Ingredient Ingredient { get; set; }
        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }
    }
}
