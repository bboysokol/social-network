using SocialNetwork.Api.Requests.CommentVM;
using SocialNetwork.Api.Requests.ReactionVMs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Api.Requests
{
    public class PostVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string CreateTime { get; set; }
        public string ImgUrl { get; set; }
        [Required]
        public string AvatarUrl { get; set; }
        public ICollection<CommentsVM> Comments { get; set; }
        public virtual ICollection<ReactionVM> Reactions { get; set; }

    }
}
