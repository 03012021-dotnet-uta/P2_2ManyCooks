using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Tag
    {
        public Tag()
        {
            RecipeTags = new HashSet<RecipeTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }

        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
