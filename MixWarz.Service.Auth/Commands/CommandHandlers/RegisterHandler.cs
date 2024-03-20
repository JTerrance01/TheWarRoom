using MediatR;
using Microsoft.AspNetCore.Identity;
using MixWarz.Service.Auth.Data;
using MixWarz.Service.Auth.Models;
using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Commands.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterHandler(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new()
            {
                UserName = request.registrationRequestDto.Email,
                Email = request.registrationRequestDto.Email,
                NormalizedEmail = request.registrationRequestDto.Email.ToUpper(),
                Name = request.registrationRequestDto.Name,
                PhoneNumber = request.registrationRequestDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.registrationRequestDto.Password);
            if (result.Succeeded)
            {
                return new ResponseDto() { IsSuccess = true, Message = "Registration Successful" };
            }
            else
            {
                return new ResponseDto() { IsSuccess = false, Message = "Registration Not Successful" };
            }
        }
    }
}
