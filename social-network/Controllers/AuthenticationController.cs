using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Responses.Wrappers;
using SocialNetwork_Backend.Responses.Wrappers.Factories;
using SocialNetwork_Backend.Services;
using SocialNetwork_Backend.Services.Interfaces;
using SocialNetwork_Backend.ViewModels;

namespace SocialNetwork_Backend.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public IActionResult Register(RegisterRequest request) => ResolveServiceResponse(_authenticationService.Register(request));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<JwtToken>))]
        public IActionResult Login(LoginRequest request) => ResolveServiceResponse(_authenticationService.Login(request));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public IActionResult Logout() => ResolveServiceResponse(_authenticationService.Logout());
    }
}
