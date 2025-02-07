using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;

        public UsersController(ApplicationDbContext context)
        {
            this.db = context;
        }

        //Get All users
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await db.Users.ToListAsync();
            return Ok(users);
        }


        //Create new User
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(User obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model State is not Valid!!");
            }
             db.Users.Add(obj);
             await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = obj.UserId }, obj);

        }


        //Get a specific users by id
        [HttpGet()]
        [Route("GetUsersById")]
        public async Task<IActionResult> GetUsersById(Guid id)
        {
            var user = await db.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails(Guid id, User obj)
        { 
            
            if(id != obj.UserId)
            {
                return BadRequest("User ID MisMatched");
            }
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(obj);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        { 
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok();
        }


    }
}
