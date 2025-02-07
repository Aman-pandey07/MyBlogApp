using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Comment should be between 1 and 500 characters")]
        public string CommentContent { get; set; } = string.Empty;

        [Required]
        [ForeignKey("User")]
        public Guid CommentedUserId { get; set; } // Foreign Key

        public User CommentedUser { get; set; } = null!; // Navigation Property

        public string CommentedUserName { get; set; } = string.Empty; // Denormalization

        [Required]
        [ForeignKey("Blog")]
        public Guid BlogId { get; set; } // Foreign Key

        public Blog Blog { get; set; } = null!; // Navigation Property

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp
    }
}
