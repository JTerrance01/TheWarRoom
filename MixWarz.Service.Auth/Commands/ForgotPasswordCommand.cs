using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MixWarz.Service.Auth.Models;
using MixWarz.Service.Auth.Models.Dto;
using MixWarz.Service.Auth.Services.IService;
using System.Text;

namespace MixWarz.Service.Auth.Commands
{
    public record ForgotPasswordCommand(ForgotPasswordDto forgotPasswordDto) : IRequest<ForgotPasswordDto>
    {
    }

    public class ForgetPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordDto>
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgetPasswordCommandHandler(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<ForgotPasswordDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.forgotPasswordDto.Email);
            if (user == null)
            {
                request.forgotPasswordDto.IsSuccess = false;
                request.forgotPasswordDto.Message = "User with this email does not exist";
                return request.forgotPasswordDto;
            }            

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{request.forgotPasswordDto.ClientURI}/ResetPassword?email={request.forgotPasswordDto.Email}&token={validToken}";

            request.forgotPasswordDto.IsSuccess = true;
            request.forgotPasswordDto.Message = "Reset Password URL has been sent to the email successfully";

            //Set up email service here

            //await SendEmail(user.Email, url, "Reset Password");

            return request.forgotPasswordDto;

        }
    }
}
