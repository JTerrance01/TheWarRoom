using MediatR;
using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Commands
{
    public record AssignRoleCommand(RegistrationRequestDto model) : IRequest<ResponseDto>
    {
    }
}
