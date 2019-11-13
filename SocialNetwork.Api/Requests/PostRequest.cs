using System;

namespace SocialNetwork.Api.Requests
{
    public class PostRequest
    {
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string ImgUrl { get; set; }
    }
}
