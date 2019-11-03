using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels.UserVMs
{
    public class EditUserViewModel
    {
        public IFormFile AvatarImage { get; set; }
    }
}
