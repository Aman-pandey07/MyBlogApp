using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    public class Blogs
    {
        [Key]
        public Guid BlogId { get; set; }

        [Required]
        public required string BlogTitle { get; set; }

        [Required]
        public required string BlogContent { get; set; }

        public string? BlogImage { get; set; }


        // Foreign key to User
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public required User Author { get; set; }  // Navigation property

        // Navigation property for comments
        public ICollection<Comments> Comments { get; set; } = new List<Comments>(); // One Blog -> Many Comments


    }
}
