using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Requests
{
    public class ReactionRequest
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

    }
}
