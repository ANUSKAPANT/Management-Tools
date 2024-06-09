using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/todo-profile")]
  [ApiController]
  public class TodoProfileController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly ITodoRepository _todoRepo;
    private readonly ITodoProfileRepository _todoProfileRepo;

    public TodoProfileController(UserManager<AppUser> userManager,
    ITodoRepository todoRepo, ITodoProfileRepository todoProfileRepo)
    {
      _userManager = userManager;
      _todoRepo = todoRepo;
      _todoProfileRepo = todoProfileRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserTodoProfile()
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var userTodoProfile = await _todoProfileRepo.GetUserTodoProfile(appUser);
      return Ok(userTodoProfile);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddTodoProfile([FromBody] int id)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var todo = await _todoRepo.GetTodoAsync(id);

      if (todo == null) return BadRequest("Todo not found");

      var userTodoProfile = await _todoProfileRepo.GetUserTodoProfile(appUser);

      if (userTodoProfile.Any(e => e.Id == id)) return BadRequest("Cannot add same todo to todoProfile");

      var todoProfileModel = new TodoProfile
      {
        TodoId = todo.Id,
        AppUserId = appUser.Id
      };

      await _todoProfileRepo.CreateAsync(todoProfileModel);

      if (todoProfileModel == null)
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
    public async Task<IActionResult> DeleteTodoProfile([FromRoute]int id)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);

      var userTodoProfile = await _todoProfileRepo.GetUserTodoProfile(appUser);

      var filteredTodo = userTodoProfile.Where(s => s.Id == id).ToList();

      if (filteredTodo.Count() == 1)
      {
        await _todoProfileRepo.DeleteTodoProfile(appUser, id);
      }
      else
      {
        return BadRequest("Todo not in your todoProfile");
      }

      return Ok();
    }

  }
}