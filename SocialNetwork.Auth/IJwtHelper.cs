namespace SocialNetwork.Auth.Models
{
    public interface IJwtHelper
    {
        JwtToken GenerateJwtToken(string UserName);
    }
}
