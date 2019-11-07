using System.Collections.Generic;

namespace SocialNetwork.Data.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }
        public UserVM Author { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<CommentVM> Comments { get; set; }
        public virtual ICollection<ReactionVM> Reactions { get; set; }

    }
}
