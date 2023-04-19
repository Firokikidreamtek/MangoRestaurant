using Microsoft.AspNetCore.Identity;

namespace Mango.Services.ProductAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}