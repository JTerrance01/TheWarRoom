using MediatR;
using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Commands
{
    public record RegisterCommand(RegistrationRequestDto registrationRequestDto) : IRequest<ResponseDto>;    
}
