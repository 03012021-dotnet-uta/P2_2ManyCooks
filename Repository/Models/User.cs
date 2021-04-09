using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable

namespace Repository.Models
{
    public partial class User
    {
        public User()
        {
            RecipeAuthors = new HashSet<RecipeAuthor>();
            Reviews = new HashSet<Review>();
            UserSearchHistories = new HashSet<UserSearchHistory>();
            UserViewHistories = new HashSet<UserViewHistory>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastAccessed { get; set; }
        public int? PermissionId { get; set; }
        public string Auth0 { get; set; }
        public string ImageUrl { get; set; }

        public virtual UserPermission Permission { get; set; }
        public virtual ICollection<RecipeAuthor> RecipeAuthors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserSearchHistory> UserSearchHistories { get; set; }
        public virtual ICollection<UserViewHistory> UserViewHistories { get; set; }

        public static implicit operator Task<object>(User v)
        {
            throw new NotImplementedException();
        }
    }
}
