using MediatR;
using MixWarz.Service.Auth.Models.Dto;

namespace MixWarz.Service.Auth.Querys
{
    public record LoginQuery(LoginRequestDto model) : IRequest<LoginResponseDto>
    {
    }
}
