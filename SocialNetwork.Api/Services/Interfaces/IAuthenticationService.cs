using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Api.ViewModels;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> Register(RegisterRequest request);

        Task<ServiceResponse<JwtToken>> Login(LoginRequest request);

        Task<ServiceResponse<bool>> Logout();
    }
}
