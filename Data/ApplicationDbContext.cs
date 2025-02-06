using Microsoft.EntityFrameworkCore;
using MyBlogApp.Models;

namespace MyBlogApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.CommentedUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CommentedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
