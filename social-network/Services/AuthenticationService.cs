using SocialNetwork_Backend.Services.Interfaces;
using SocialNetwork_Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Services.ServiceResponses;
using SocialNetwork_Backend.ViewModels;
using SocialNetwork_Backend.Database;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork_Backend.Services
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

        public ServiceResponse<bool> Register(RegisterRequest request)
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

            var result = _userManager.CreateAsync(user, request.Password);

            if (result.IsFaulted)
            {
                return ServiceResponse<bool>.Error();
            }
            return ServiceResponse<bool>.Ok();
        }

        public ServiceResponse<JwtToken> Login(LoginRequest request)
        {
            var result = _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);

            if (result.IsFaulted)
                return ServiceResponse<JwtToken>.Error("Login failed");

            var token = _jwtHelper.GenerateJwtToken(request.UserName);
            if (token == null)
            {
                return ServiceResponse<JwtToken>.Error("User doesn't exist");
            }

            return ServiceResponse<JwtToken>.Ok(token);
        }

        public ServiceResponse<bool> Logout()
        {
            var result = _signInManager.SignOutAsync();
            if(result.IsFaulted)
                return ServiceResponse<bool>.Error();
            return ServiceResponse<bool>.Ok();

        }


    }
}
