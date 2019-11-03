using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Requests
{
    public class AddCommentVM
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; }
    }
}
