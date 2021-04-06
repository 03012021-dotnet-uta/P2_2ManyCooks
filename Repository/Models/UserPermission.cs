using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class UserPermission
    {
        public UserPermission()
        {
            Users = new HashSet<User>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
