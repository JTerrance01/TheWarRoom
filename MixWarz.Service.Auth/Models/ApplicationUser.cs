using Microsoft.AspNetCore.Identity;

namespace MixWarz.Service.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }    
}
