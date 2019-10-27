using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.ViewModels.ReactionVMs
{
    public class ReactionVM
    {
        [Required]
        public string Author { get; set; }
        
    }
}
