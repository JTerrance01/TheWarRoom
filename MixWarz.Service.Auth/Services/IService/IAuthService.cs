using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Services.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto);    
    }
}
