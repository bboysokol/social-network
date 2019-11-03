using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Responses.Wrappers;
using SocialNetwork.Api.Responses.Wrappers.Factories;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Api.Requests;
using SocialNetwork.Auth.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register(RegisterRequest request) => ResolveServiceResponse(await _authenticationService.Register(request));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<JwtToken>))]
        public async Task<IActionResult> Login(LoginRequest request) => ResolveServiceResponse(await _authenticationService.Login(request));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public async Task<IActionResult> Logout() => ResolveServiceResponse(await _authenticationService.Logout());
    }
}
