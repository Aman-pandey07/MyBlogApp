using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class BlogsController : Controller
    //{
    //    private readonly ApplicationDbContext db;

    //    public BlogsController(ApplicationDbContext context)
    //    {
    //        this.db = context;
    //    }


    //    //Get All users
    //    [HttpGet]
    //    [Route("GetBlogs")]
    //    public async Task<IActionResult> GetBlogs()
    //    {
    //        var blogs = await db.Blogs.ToListAsync();
    //        return Ok(blogs);
    //    }


    //    [HttpPost]
    //    [Route("CreateBlogs")]
    //    public async Task<IActionResult> CreateBlogs(Blog obj)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest("Model State is not Valid!!");
    //        }
    //        db.Blogs.Add(obj);
    //        await db.SaveChangesAsync();
    //        return CreatedAtAction(nameof(GetBlogs), new { id = obj.BlogId }, obj);
    //    }

    //    //Get a specific users by id
    //    [HttpGet()]
    //    [Route("GetBlogById")]
    //    public async Task<IActionResult> GetBlogById(Guid id)
    //    {
    //        var blog = await db.Blogs.FindAsync(id);
    //        if (blog == null)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(blog);
    //    }


    //    [HttpPut]
    //    [Route("UpdateBlogDetails")]
    //    public async Task<IActionResult> UpdateBlogDetails(Guid id, Blog obj)
    //    {

    //        if (id != obj.BlogId)
    //        {
    //            return BadRequest("Blog ID MisMatched");
    //        }
    //        db.Entry(obj).State = EntityState.Modified;
    //        await db.SaveChangesAsync();
    //        return Ok(obj);
    //    }

    //    [HttpDelete]
    //    [Route("DeleteBlog")]
    //    public async Task<IActionResult> DeleteBlog(Guid id)
    //    {
    //        var blog = await db.Blogs.FindAsync(id);
    //        if (blog == null)
    //        {
    //            return NotFound();
    //        }
    //        db.Blogs.Remove(blog);
    //        await db.SaveChangesAsync();
    //        return Ok();
    //    }
    //}


    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BlogsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // Get All Blogs
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _db.Blogs.Include(b => b.User).ToListAsync();
            return Ok(blogs);
        }

        // Create Blog
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Blog obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Blogs.Add(obj);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlogById), new { id = obj.BlogId }, obj);
        }

        // Get Blog by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var blog = await _db.Blogs.Include(b => b.User).FirstOrDefaultAsync(b => b.BlogId == id);
            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        // Update Blog
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] Blog obj)
        {
            if (id != obj.BlogId)
                return BadRequest("Blog ID Mismatch");

            _db.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(obj);
        }

        // Delete Blog
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _db.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            _db.Blogs.Remove(blog);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
