using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlogApp.Data
{
    public class ApplicationAuthDbContext : IdentityDbContext<ApplicationUsers>
    {
        public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : base(options)
        {
        }
    }
}
