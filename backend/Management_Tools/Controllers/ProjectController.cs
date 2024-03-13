using System;
using Microsoft.AspNetCore.Mvc;
using Management_Tools.Models;
using Management_Tools.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Management_Tools.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : Controller
	{
        private readonly ApplicationDbContext _db;

        public ProjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            var projects = _db.Projects
                .Include(p => p.Todos)
                .ThenInclude(p => p.Users)
                    .Include(t => t.Users) // Include related Users for each Todo
                .ToList();


            return Ok(projects);
        }


        [HttpPost]
        public ActionResult<Project> CreateProject([FromBody] Project project)
        {
            if (project == null) return BadRequest();
            if (project.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            Project newProject = new Project
            {

                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                Todos = project.Todos
            };

            _db.Projects.Add(newProject);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("id")]
        public ActionResult<Project> GetProject(int id)
        {
            if (id == 0) return BadRequest();
            var project = _db.Projects.FirstOrDefault(proj => proj.Id == id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpDelete("id")]
        public IActionResult DeleteProject(int id)
        {
            if (id == 0) return BadRequest();
            var project = _db.Projects.FirstOrDefault(proj => proj.Id == id);
            if (project == null)
                return NotFound();
            _db.Projects.Remove(project);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProject(int id, [FromBody] Project project)
        {
            if (project == null || project.Id != id) return BadRequest();
            _db.Projects.Update(project);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPatch("id")]
        public IActionResult UpdateProject(int id, JsonPatchDocument<Project> project)
        {
            if (project == null || id == 0) return BadRequest();
            var proj = _db.Projects.FirstOrDefault(u => u.Id == id);


            if (proj == null) return BadRequest();

            project.ApplyTo(proj, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Projects.Update(proj);
            _db.SaveChanges();
            return NoContent();
        }

    }
}

