using SocialNetwork.Api.Models;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Api.ViewModels;

namespace SocialNetwork.Api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        ServiceResponse<bool> Register(RegisterRequest request);

        ServiceResponse<JwtToken> Login(LoginRequest request);

        ServiceResponse<bool> Logout();
    }
}
