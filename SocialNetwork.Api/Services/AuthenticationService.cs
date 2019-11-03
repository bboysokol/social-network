using SocialNetwork.Api.Services.Interfaces;
using System.Linq;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Api.Requests;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Database;
using SocialNetwork.Auth.Models;

namespace SocialNetwork.Api.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly SignInManager<User> _signInManager = null;
        private readonly UserManager<User> _userManager = null;
        private readonly IJwtHelper _jwtHelper;


        public AuthenticationService(SocialNetworkContext context, SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtHelper jwtHelper) : base(context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<ServiceResponse<bool>> Register(RegisterRequest request)
        {
            if (Context.Users.Any(i => i.Email == request.Email))
                return ServiceResponse<bool>.Error("Email Error");
            if (request.Password != request.ConfirmedPassword)
                return ServiceResponse<bool>.Error("Passwords are not the same");

            var user = new User()
            {
                Email = request.Email,
                UserName = request.UserName,
                RegisterDate = request.RegisterDate,
                AvatarUrl = request.AvatarUrl
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return ServiceResponse<bool>.Error();
            }
            return ServiceResponse<bool>.Ok();
        }

        public async Task<ServiceResponse<JwtToken>> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);

            if (!result.Succeeded)
                return ServiceResponse<JwtToken>.Error("Login failed");

            var token = _jwtHelper.GenerateJwtToken(request.UserName);
            if (token == null)
            {
                return ServiceResponse<JwtToken>.Error("User doesn't exist");
            }

            return ServiceResponse<JwtToken>.Ok(token);
        }

        public async Task<ServiceResponse<bool>> Logout()
        {
            await _signInManager.SignOutAsync();
            return ServiceResponse<bool>.Ok();

        }


    }
}
