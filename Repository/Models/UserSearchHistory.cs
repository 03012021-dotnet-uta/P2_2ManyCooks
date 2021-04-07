using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class UserSearchHistory
    {
        public int HistoryId { get; set; }
        public string SearchString { get; set; }
        public DateTime SearchDate { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
