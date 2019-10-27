using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.ViewModels.UserVMs
{
    public class UserVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string AvatarUrl { get; set; }


    }
}
