using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        [Required]
        public string CreateTime { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string AvatarUrl { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Reaction> Reactions{ get; set; } = new HashSet<Reaction>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    }
}
