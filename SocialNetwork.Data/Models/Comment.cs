using System;

namespace SocialNetwork.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; } = String.Format("{0:g}", DateTime.Now);
        public bool IsDeleted { get; set; } = false;
    }
}
