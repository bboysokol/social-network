using SocialNetwork.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services.Interfaces
{
    public interface IJwtHelper
    {
        JwtToken GenerateJwtToken(string UserName);
    }
}
