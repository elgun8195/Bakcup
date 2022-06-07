using Microsoft.AspNetCore.Identity;

namespace Fiorella_Webim.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
