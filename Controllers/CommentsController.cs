using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class CommentsController : Controller
    //{
    //    private readonly ApplicationDbContext db;

    //    public CommentsController(ApplicationDbContext context)
    //    {
    //        this.db = context;
    //    }



    //    [HttpPost]
    //    [Route("AddComment")]
    //    public async Task<IActionResult> AddComment(Comment obj)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest("Model State is not Valid!!");
    //        }
    //        db.Comments.Add(obj);
    //        await db.SaveChangesAsync();
    //        return Ok(obj);
    //    }

    //    //Get a specific users by id
    //    [HttpGet()]
    //    [Route("GetCommentById")]
    //    public async Task<IActionResult> GetCommentById(Guid id)
    //    {
    //        var comment = await db.Comments.FindAsync(id);
    //        if (comment == null)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(comment);
    //    }


    //    [HttpPut]
    //    [Route("UpdateComment")]
    //    public async Task<IActionResult> UpdateComment(Guid id, Comment obj)
    //    {

    //        if (id != obj.CommentId)
    //        {
    //            return BadRequest("Blog ID MisMatched");
    //        }
    //        db.Entry(obj).State = EntityState.Modified;
    //        await db.SaveChangesAsync();
    //        return Ok(obj);
    //    }

    //    [HttpDelete]
    //    [Route("DeleteComment")]
    //    public async Task<IActionResult> DeleteComment(Guid id)
    //    {
    //        var comment = await db.Comments.FindAsync(id);
    //        if (comment == null)
    //        {
    //            return NotFound();
    //        }
    //        db.Comments.Remove(comment);
    //        await db.SaveChangesAsync();
    //        return Ok();
    //    }
    //}

    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CommentsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // Get All Comments for a Blog
        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetCommentsByBlog(Guid blogId)
        {
            var comments = await _db.Comments.Where(c => c.BlogId == blogId).Include(c => c.Blog).ToListAsync();
            return Ok(comments);
        }

        // Get Comment by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // Add Comment
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Comments.Add(obj);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommentById), new { id = obj.CommentId }, obj);
        }

        // Update Comment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] Comment obj)
        {
            if (id != obj.CommentId)
                return BadRequest("Comment ID Mismatch");

            _db.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(obj);
        }

        // Delete Comment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
