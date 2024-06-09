using System;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Mappers;
using api.Dtos;


namespace api.Controllers
{
    [Route("api/Projects")]
    [ApiController]
    public class ProjectController : ControllerBase
	{
        private readonly IProjectRepository _projectRepo;
        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var projects = await _projectRepo.GetProjectsAsync();
            var projectdtos = projects.Select(p => p.ToProjectDto());
            return Ok(projectdtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequestDto project)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var projectModel = await _projectRepo.CreateProjectAsync(project);
            return Ok(projectModel.ToProjectDto());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var project = await _projectRepo.GetProjectAsync(id);
            if(project == null) return NotFound();
            return Ok(project.ToProjectDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto updatedto)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var project = await _projectRepo.UpdateProjectAsync(id, updatedto);
            if(project == null) return NotFound();
            return Ok(project.ToProjectDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute]int id)
        {
            var project = await _projectRepo.DeleteProjectAsync(id);
            if (project == null) return NotFound();
            return NoContent();
        }

    }
}

