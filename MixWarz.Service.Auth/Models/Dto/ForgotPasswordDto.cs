namespace MixWarz.Service.Auth.Models.Dto
{
    public class ForgotPasswordDto : ResponseDto
    {        
        public string Email { get; set; }
        public string ClientURI { get; set; }
    }
}
