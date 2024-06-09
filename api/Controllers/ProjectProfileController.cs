using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/project-profile")]
  [ApiController]
  public class ProjectProfileController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IProjectRepository _projectRepo;
    private readonly IProjectProfileRepository _projectProfileRepo;

    public ProjectProfileController(UserManager<AppUser> userManager,
    IProjectRepository projectRepo, IProjectProfileRepository projectProfileRepo)
    {
      _userManager = userManager;
      _projectRepo = projectRepo;
      _projectProfileRepo = projectProfileRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserProjectProfile()
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var userProjectProfile = await _projectProfileRepo.GetUserProjectProfile(appUser);
      return Ok(userProjectProfile);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddProjectProfile([FromBody] int id)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var project = await _projectRepo.GetProjectAsync(id);

      if (project == null) return BadRequest("Project not found");

      var userProjectProfile = await _projectProfileRepo.GetUserProjectProfile(appUser);

      if (userProjectProfile.Any(e => e.Id == id)) return BadRequest("Cannot add same project to projectProfile");

      var projectProfileModel = new ProjectProfile
      {
        ProjectId = project.Id,
        AppUserId = appUser.Id
      };

      await _projectProfileRepo.CreateAsync(projectProfileModel);

      if (projectProfileModel == null)
      {
        return StatusCode(500, "Could not create");
      }
      else
      {
        return Created();
      }
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteProjectProfile([FromRoute]int id)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);

      var userProjectProfile = await _projectProfileRepo.GetUserProjectProfile(appUser);

      var filteredProject = userProjectProfile.Where(s => s.Id == id).ToList();

      if (filteredProject.Count() == 1)
      {
        await _projectProfileRepo.DeleteProjectProfile(appUser, id);
      }
      else
      {
        return BadRequest("Project not in your projectProfile");
      }

      return Ok();
    }

  }
}