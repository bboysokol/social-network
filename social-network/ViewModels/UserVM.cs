using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.ViewModels.UserVMs
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string AvatarUrl { get; set; }


    }
}
