using Microsoft.AspNetCore.Identity;

namespace WebBanNuoc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
    }
}
