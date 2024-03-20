using MixWarz.Service.Auth.Models;

namespace MixWarz.Service.Auth.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
