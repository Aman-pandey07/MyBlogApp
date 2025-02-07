using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MyBlogApp.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Name should be between 3 and 200 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string UserPhone { get; set; } = string.Empty; // Changed to string

        public string? ProfilePicture { get; set; } // Optional Profile Picture URL

        [Required]
        public bool IsAuthor { get; set; } // True if the user is an author

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp

        // Relationships
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
