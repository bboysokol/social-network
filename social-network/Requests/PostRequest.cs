using SocialNetwork.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels
{
    public class PostRequest
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string ImgUrl { get; set; }
        public string CreateTime { get; set; } = String.Format("{0:g}", DateTime.Now);
    }
}
