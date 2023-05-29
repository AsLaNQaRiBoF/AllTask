using Microsoft.AspNetCore.Identity;

namespace NestApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Name {get; set;}
        public string SurName { get; set; }
        public string IsActive { get; set; }
    }
}
