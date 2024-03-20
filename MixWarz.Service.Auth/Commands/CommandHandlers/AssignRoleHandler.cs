using MediatR;
using Microsoft.AspNetCore.Identity;
using MixWarz.Service.Auth.Commands;
using MixWarz.Service.Auth.Data;
using MixWarz.Service.Auth.Models;
using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Querys.QueryHandlers
{
    public class AssignRoleHandler : IRequestHandler<AssignRoleCommand, ResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AssignRoleHandler(RoleManager<IdentityRole> roleManager, 
            AppDbContext db, 
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ResponseDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == request.model.Email.ToLower());
            var response = new ResponseDto();
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(request.model.Role).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(request.model.Role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, request.model.Role);
                response.IsSuccess = true;
                response.Message = "Role Selected Successfully";
                return response;
            }
            response.IsSuccess = false;
            response.Message = "Assigning Role Not Successful";
            return response;
        }
    }
}
