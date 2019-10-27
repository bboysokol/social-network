using SocialNetwork_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Services.Interfaces
{
    public interface IJwtHelper
    {
        JwtToken GenerateJwtToken(string UserName);
    }
}