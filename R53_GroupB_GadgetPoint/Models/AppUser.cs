using Microsoft.AspNetCore.Identity;

namespace R53_GroupB_GadgetPoint.Models
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public Address? Address { get; set; }
    }
}