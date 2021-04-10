using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Repository.Models
{
    public partial class RecipeTag
    {
        public int RecipeId { get; set; }
        public int TagId { get; set; }

        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }
        [JsonIgnore]
        public virtual Tag Tag { get; set; }
    }
}
