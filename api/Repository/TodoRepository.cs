using System;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Dtos;
using Microsoft.AspNetCore.Identity;
using api.Mappers;
namespace api.Repository
{
  public class TodoRepository : ITodoRepository
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public TodoRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }
    public async Task<Todo?> CreateTodoAsync(CreateTodoRequestDto todo, int projectId)
    {
      var userIds = todo.UserIds;
      var todoModel = todo.ToTodoFromCreateDto(projectId);
      using var transaction = _context.Database.BeginTransaction();
      try
      {
        await _context.Todos.AddAsync(todoModel);
        await _context.SaveChangesAsync();
        foreach (string id in userIds)
        {
          if (!todoModel.TodoProfiles.Any(p => p.AppUserId == id))
          {
            var appUser = await _userManager.FindByIdAsync(id);
            if(appUser == null) return null;
            var todoProfile = new TodoProfile { TodoId = todoModel.Id, AppUserId = appUser.Id };
            await _context.TodoProfiles.AddAsync(todoProfile);
          }
        }
        await _context.SaveChangesAsync();
        transaction.Commit();
      }
      catch (Exception)
      {
        return null;
      }
      return todoModel;
    }

    public async Task<Todo?> DeleteTodoAsync(int id)
    {
      var todo = await _context.Todos.Include(t => t.TodoProfiles).FirstOrDefaultAsync(p => p.Id == id);
      if(todo == null) return null;
      _context.Remove(todo);
      await _context.SaveChangesAsync();
      return todo;
    }

    public async Task<Todo?> GetTodoAsync(int id)
    {
      var todo = await _context.Todos.Include(t => t.TodoProfiles).ThenInclude(u => u.AppUser).FirstOrDefaultAsync(p => p.Id == id);
      return todo;
    }

    public async Task<List<Todo>> GetTodosAsync()
    {
      var todos = await _context.Todos.Include(t => t.TodoProfiles).ThenInclude(u => u.AppUser).ToListAsync();
      return todos;
    }

    public async Task<Todo?> UpdateTodoAsync(int id, UpdateTodoRequestDto tododto)
    {
      var todo = await _context.Todos.Include(t => t.TodoProfiles).ThenInclude(u => u.AppUser).FirstOrDefaultAsync(p => p.Id == id);
      if(todo == null) return null;
      var userIds = tododto.UserIds;
      
      //removing delete profile
      todo.TodoProfiles.RemoveAll(t => !userIds.Contains(t.AppUserId));

      var newUserIds = userIds.Where(id => !todo.TodoProfiles.Any(t => t.AppUserId == id)).ToList();

      foreach(string userId in newUserIds)
      {
        var appUser = await _userManager.FindByIdAsync(userId);
        if(appUser == null) continue;
        var todoProfile = new TodoProfile { TodoId = todo.Id, AppUserId = appUser.Id };
        todo.TodoProfiles.Add(todoProfile);
      };

      todo.TodoDescription = tododto.TodoDescription;
      todo.TodoName = tododto.TodoName;
      todo.Status = tododto.Status;
      
      await _context.SaveChangesAsync();
      return todo;
    }
  }
}