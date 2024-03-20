using MediatR;
using Microsoft.AspNetCore.Identity;
using MixWarz.Service.Auth.Data;
using MixWarz.Service.Auth.Models;
using MixWarz.Service.Auth.Models.Dto;
using MixWarz.Service.Auth.Services.IService;

namespace MixWarz.Service.Auth.Querys.QueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;        
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(AppDbContext db,
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == request.model.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, request.model.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDTO = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }
    }
}
