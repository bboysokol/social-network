using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Services.ServiceResponses;
using SocialNetwork_Backend.ViewModels;

namespace SocialNetwork_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        ServiceResponse<bool> Register(RegisterRequest request);

        ServiceResponse<JwtToken> Login(LoginRequest request);

        ServiceResponse<bool> Logout();
    }
}
