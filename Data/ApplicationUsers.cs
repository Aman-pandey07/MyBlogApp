using Microsoft.AspNetCore.Identity;

namespace MyBlogApp.Data
{
    public class ApplicationUsers: IdentityUser
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
