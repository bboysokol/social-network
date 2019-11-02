using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels.FriendVMs
{
    public class FriendshipVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FriendId { get; set; }
    }
}
