using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Models
{
    public class JwtToken
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
