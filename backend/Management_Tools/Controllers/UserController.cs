using System;
using Microsoft.AspNetCore.Mvc;
using Management_Tools.Models;
using Management_Tools.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Management_Tools.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _db.Users.Include(user => user.Projects).ThenInclude(todo => todo.Todos).ToList();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user == null) return BadRequest();
            if (user.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            User newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ImageUrl = user.ImageUrl,

            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("id")]
        public ActionResult<User> GetUser(int id)
        {
            if (id == 0) return BadRequest();
            var User = _db.Users.FirstOrDefault(proj => proj.Id == id);
            if (User == null)
                return NotFound();

            return Ok(User);
        }

        [HttpDelete("id")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0) return BadRequest();
            var User = _db.Users.FirstOrDefault(proj => proj.Id == id);
            if (User == null)
                return NotFound();
            _db.Users.Remove(User);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id) return BadRequest();
            _db.Users.Update(user);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPatch("id")]
        public IActionResult UpdateUser(int id, JsonPatchDocument<User> user)
        {
            if (user == null || id == 0) return BadRequest();
            var proj = _db.Users.FirstOrDefault(u => u.Id == id);


            if (proj == null) return BadRequest();

            user.ApplyTo(proj, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Users.Update(proj);
            _db.SaveChanges();
            return NoContent();
        }

    }
}

