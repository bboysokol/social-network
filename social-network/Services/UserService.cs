using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork_Backend.Database;
using SocialNetwork_Backend.Helpers;
using SocialNetwork_Backend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Services
{
   
    public class UserService : IUserService
    {
        private readonly SocialNetworkContext socialNetworkContext;
        private readonly AppSettings _appSettings;


        public UserService(IOptions<AppSettings> appSettings, SocialNetworkContext socialNetworkContext)
        {
            _appSettings = appSettings.Value;
            this.socialNetworkContext = socialNetworkContext;

        }

        public User Authenticate(string username)
        {
            User user = socialNetworkContext.Users.SingleOrDefault(i => i.UserName == username);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);


            user.PasswordHash = null;

            return user;
        }

    }
}
