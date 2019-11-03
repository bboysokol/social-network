using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Api.Helpers;
using SocialNetwork.Auth.Models;
using SocialNetwork.Data.Database;
using SocialNetwork.Data.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Auth
{
   
    public class JwtHelper : IJwtHelper
    {
        private readonly SocialNetworkContext _context;
        private readonly AppSettings _appSettings;


        public JwtHelper(IOptions<AppSettings> appSettings, SocialNetworkContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;

        }

        public JwtToken GenerateJwtToken(string username)
        {
            User user = _context.Users.SingleOrDefault(i => i.UserName == username);

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
            var token = new JwtToken()
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
            };
            return token;
        }

        public bool DeactivateToken(string token)
        {
            return true;
        }

    }
}
