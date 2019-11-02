using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels
{
    public class RegisterRequest : LoginRequest
    {

        [Required]
        public string ConfirmedPassword { get; set; }
        [Required]
        public string RegisterDate { get; set; }
        public string AvatarUrl { get; set; }
    }
}
