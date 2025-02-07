using Microsoft.EntityFrameworkCore;
using MyBlogApp.Models;

namespace MyBlogApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); // Delete blogs if author is deleted

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.CommentedUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CommentedUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting user if comments exist

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade); // Delete comments if blog is deleted
        }
    }
}