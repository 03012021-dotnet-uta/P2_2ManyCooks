using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class UserViewHistory
    {
        public int HistoryId { get; set; }
        public DateTime ViewDate { get; set; }
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
