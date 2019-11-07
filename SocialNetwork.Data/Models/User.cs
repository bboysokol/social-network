using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Models
{
    public class User : IdentityUser<int>
    {
        public string RegisterDate { get; set; } = String.Format("{0:g}", DateTime.Now);
        public string AvatarUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Token { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
    }
}
