using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels.FriendVMs
{
    public class FriendVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string AvatarUrl { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
