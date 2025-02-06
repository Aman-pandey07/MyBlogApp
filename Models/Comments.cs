using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    public class Comments
    {
        [Key]
        public Guid CommentId { get; set; }

        [Required]
        public required string CommentContent { get; set; }

        // Foreign key to User (Commenter)
        public Guid CommentedUserId { get; set; }

        [ForeignKey("CommentedUserId")]
        public required User CommentedUser { get; set; }

        // Foreign key to Blog
        public Guid BlogId { get; set; }
        [ForeignKey("BlogId")]
        public required Blogs Blog { get; set; }
    }
}
