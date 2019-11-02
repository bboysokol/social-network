using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Responses.Wrappers;
using SocialNetwork.Api.Responses.Wrappers.Factories;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Api.ViewModels;

namespace SocialNetwork.Api.Controllers
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
