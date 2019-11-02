using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels.ReactionVMs
{
    public class AddReactionVM
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

    }
}
