using System;
using Microsoft.AspNetCore.Mvc;
using Management_Tools.Models;
using Management_Tools.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Management_Tools.Controllers
{
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TodoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            var todos = _db.Todos.Include(todo=>todo.Project).Include(todo => todo.Users).ToList();
            return Ok(todos);
        }

        [HttpPost]
        public ActionResult<Todo> CreateTodo([FromBody] Todo todo)
        {
            if (todo == null) return BadRequest();
            if (todo.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            Todo newTodo = new Todo
            {
                TodoName = todo.TodoName,
                TodoDescription = todo.TodoDescription,
                ProjectId = todo.ProjectId,
            };

            _db.Todos.Add(newTodo);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("id")]
        public ActionResult<Todo> GetTodo(int id)
        {
            if (id == 0) return BadRequest();
            var Todo = _db.Todos.FirstOrDefault(proj => proj.Id == id);
            if (Todo == null)
                return NotFound();

            return Ok(Todo);
        }

        [HttpDelete("id")]
        public IActionResult DeleteTodo(int id)
        {
            if (id == 0) return BadRequest();
            var Todo = _db.Todos.FirstOrDefault(proj => proj.Id == id);
            if (Todo == null)
                return NotFound();
            _db.Todos.Remove(Todo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (todo == null || todo.Id != id) return BadRequest();
            _db.Todos.Update(todo);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPatch("id")]
        public IActionResult UpdateTodo(int id, JsonPatchDocument<Todo> todo)
        {
            if (todo == null || id == 0) return BadRequest();
            var proj = _db.Todos.FirstOrDefault(u => u.Id == id);


            if (proj == null) return BadRequest();

            todo.ApplyTo(proj, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Todos.Update(proj);
            _db.SaveChanges();
            return NoContent();
        }

    }
}

