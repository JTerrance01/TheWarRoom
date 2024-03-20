using MixWarz.Web.Models;

namespace MixWarz.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
