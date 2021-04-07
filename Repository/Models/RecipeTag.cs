using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class RecipeTag
    {
        public int RecipeId { get; set; }
        public int TagId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
