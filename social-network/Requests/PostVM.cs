using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.ViewModels.CommentVM;
using SocialNetwork_Backend.ViewModels.ReactionVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.ViewModels
{
    public class PostVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string AuthorId { get; set; }
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
