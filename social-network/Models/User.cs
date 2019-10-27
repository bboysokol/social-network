using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Models
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime RegisterDate { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Token { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }

        public virtual ICollection<Friend> FriendOf { get; set; }

    }
}
