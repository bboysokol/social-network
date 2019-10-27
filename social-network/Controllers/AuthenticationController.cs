using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Responses.Wrappers.Factories;
using SocialNetwork_Backend.Services;
using SocialNetwork_Backend.ViewModels;

namespace SocialNetwork_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAccountService _accountService;

        public AuthenticationController(IApiResponseFactory responseFactory) : base(responseFactory)
        {
        }

        //[Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Failure();
                }

                if (registerRequest.Password != registerRequest.ConfirmedPassword)
                {
                    return Failure();
                }

                var user = new User()
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.UserName,
                    RegisterDate = registerRequest.RegisterDate,
                    AvatarUrl = registerRequest.AvatarUrl
                };

                var registerResult = await userManager.CreateAsync(user, registerRequest.Password);

                if (!registerResult.Succeeded)
                {
                    return Failure();
                }

                return Success("Rejestracja przebiegła pomyślnie, teraz możesz się zalogować");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in Register()!");
                return Failure();
            }
        }
        //[Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Failure();
                }

                var result = await signInManager.PasswordSignInAsync(
                    loginRequest.UserName, loginRequest.Password, true, false);

                if (!result.Succeeded)
                {
                    return Failure();
                }

                var user = _userService.Authenticate(loginRequest.UserName);
                if (user == null)
                {
                    return BadRequest("User doesn't exist");
                }
                return Success(user);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Error in LogIn()");
                return Failure();
            }



        }

        //[Route("Logout")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR!, Unexcepted error in LogOut()");
                return Failure();
            }
        }
        [HttpPost]
        public async Task<IActionResult> IsLoggedIn()
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    if (user != null)
                    {
                        return Success(new { Email = HttpContext.User.Identity.Name });
                    }

                    await signInManager.SignOutAsync();
                    return Failure();
                }
                else
                {
                    return Failure();
                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in IsLoggedIn()");
                return Failure();
            }
        }

    }
}
