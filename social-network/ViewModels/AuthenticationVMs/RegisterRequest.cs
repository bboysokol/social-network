using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.ViewModels
{
    public class RegisterRequest : LoginRequest
    {

        [Required]
        public string ConfirmedPassword { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string AvatarUrl { get; set; }
    }
}