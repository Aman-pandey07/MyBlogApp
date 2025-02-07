using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    public class Blog
    {
        [Key]
        public Guid BlogId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Title should be between 3 and 200 characters")]
        public string BlogTitle { get; set; } = string.Empty;

        [Required]
        public string BlogContent { get; set; } = string.Empty;

        public string? BlogImage { get; set; } // Optional Image URL

        [Required]
        public string AutherUserName { get; set; } = string.Empty; // Denormalization for easy access

        [Required]
        [ForeignKey("User")]
        public Guid AuthorId { get; set; } // Foreign Key

        public User Author { get; set; } = null!; // Navigation Property

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp

        // Relationships
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


        // Foreign Key for User
        public Guid UserId { get; set; }  // Make sure this exists

        // Navigation Property
        public User? User { get; set; }  // Add this if missing
    }
}
