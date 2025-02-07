using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UsersController : Controller
    //{
    //    private readonly ApplicationDbContext db;

    //    public UsersController(ApplicationDbContext context)
    //    {
    //        this.db = context;
    //    }

    //    //Get All users
    //    [HttpGet]
    //    [Route("GetUsers")]
    //    public async Task<IActionResult> GetUsers()
    //    {
    //        var users = await db.Users.ToListAsync();
    //        return Ok(users);
    //    }


    //    //Create new User
    //    [HttpPost]
    //    [Route("RegisterUser")]
    //    public async Task<IActionResult> RegisterUser(User obj)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest("Model State is not Valid!!");
    //        }
    //         db.Users.Add(obj);
    //         await db.SaveChangesAsync();
    //         return CreatedAtAction(nameof(GetUsers), new { id = obj.UserId }, obj);

    //    }


    //    //Get a specific users by id
    //    [HttpGet()]
    //    [Route("GetUsersById")]
    //    public async Task<IActionResult> GetUsersById(Guid id)
    //    {
    //        var user = await db.Users.FindAsync(id);
    //        if(user == null)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(user);
    //    }


    //    [HttpPut]
    //    [Route("UpdateUserDetails")]
    //    public async Task<IActionResult> UpdateUserDetails(Guid id, User obj)
    //    { 

    //        if(id != obj.UserId)
    //        {
    //            return BadRequest("User ID MisMatched");
    //        }
    //        db.Entry(obj).State = EntityState.Modified;
    //        await db.SaveChangesAsync();
    //        return Ok(obj);
    //    }

    //    [HttpDelete]
    //    [Route("DeleteUser")]
    //    public async Task<IActionResult> DeleteUser(Guid id)
    //    { 
    //        var user = await db.Users.FindAsync(id);
    //        if (user == null)
    //        {
    //            return NotFound();
    //        }
    //        db.Users.Remove(user);
    //        await db.SaveChangesAsync();
    //        return Ok();
    //    }


    //}


    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext context)
        {
            _db = context;
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        // Register User
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Users.Add(obj);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = obj.UserId }, obj);
        }

        // Get User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Update User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User obj)
        {
            if (id != obj.UserId)
                return BadRequest("User ID Mismatch");

            _db.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(obj);
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
