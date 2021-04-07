using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewRating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
