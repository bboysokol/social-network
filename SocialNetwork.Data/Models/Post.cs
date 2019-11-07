using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string CreatedAt { get; set; } = String.Format("{0:g}", DateTime.Now);
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
