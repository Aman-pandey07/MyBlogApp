using System.ComponentModel.DataAnnotations;

namespace MyBlogApp.Models
{
    public class User
    {

        [Key]
        public Guid UserId { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Name should be between 3 and 200 characters")]
        public required string UserName { get; set; }

        [Required, EmailAddress]  
        public required string UserEmail { get; set; }

        [Required]
        public required long UserPhoneNumber { get; set; }

        // TODO Implementation of Dp is left please keep a note of it

        public bool isUserAutherOrUser { get; set; }


        // Navigation properties
        public ICollection<Blogs> Blogs { get; set; } = new List<Blogs>(); // One User -> Many Blogs
        public ICollection<Comments> Comments { get; set; } = new List<Comments>(); // One User -> Many Comments
    }
}
