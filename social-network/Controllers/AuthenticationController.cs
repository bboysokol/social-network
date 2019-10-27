using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Responses.Wrappers;
using SocialNetwork_Backend.Responses.Wrappers.Factories;
using SocialNetwork_Backend.Services;
using SocialNetwork_Backend.Services.Interfaces;
using SocialNetwork_Backend.ViewModels;

namespace SocialNetwork_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        
        public AuthenticationController(IApiResponseFactory responseFactory, IAuthenticationService authenticationService) : base(responseFactory)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<JwtToken>))]
        public async Task<IActionResult> Register(RegisterRequest request) => ResolveServiceResponse(_authenticationService.Register(request));

        //    //[Route("Login")]
        //    [HttpPost]
        //    public async Task<IActionResult> Login(LoginRequest loginRequest)
        //    {

        //        try
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return Failure();
        //            }

        //            var result = await signInManager.PasswordSignInAsync(
        //                loginRequest.UserName, loginRequest.Password, true, false);

        //            if (!result.Succeeded)
        //            {
        //                return Failure();
        //            }

        //            var user = _userService.Authenticate(loginRequest.UserName);
        //            if (user == null)
        //            {
        //                return BadRequest("User doesn't exist");
        //            }
        //            return Success(user);
        //        }
        //        catch (Exception ex)
        //        {

        //            logger.LogError(ex, "Error in LogIn()");
        //            return Failure();
        //        }



        //    }

        //    //[Route("Logout")]
        //    [HttpPost]
        //    [Authorize]
        //    public async Task<IActionResult> LogOut()
        //    {
        //        try
        //        {
        //            await signInManager.SignOutAsync();
        //            return Success();
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.LogError(ex, "ERROR!, Unexcepted error in LogOut()");
        //            return Failure();
        //        }
        //    }
        //}
    }
}
