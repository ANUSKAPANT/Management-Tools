using System;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class TodoProfileRepository : ITodoProfileRepository
  {
    private readonly ApplicationDbContext _context;
    public TodoProfileRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<TodoProfile> CreateAsync(TodoProfile todoProfile)
    {
      await _context.TodoProfiles.AddAsync(todoProfile);
      await _context.SaveChangesAsync();
      return todoProfile;
    }

    public async Task<TodoProfile> DeleteTodoProfile(AppUser appUser, int todoId)
    {
      var todoProfileModel = await _context.TodoProfiles.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Todo.Id == todoId);

      if (todoProfileModel == null)
      {
        return null;
      }

      _context.TodoProfiles.Remove(todoProfileModel);
      await _context.SaveChangesAsync();
      return todoProfileModel;
    }

    public async Task<List<Todo>> GetUserTodoProfile(AppUser user)
    {
      return await _context.TodoProfiles.Where(u => u.AppUserId == user.Id)
      .Select(todo => new Todo
      {
        Id = todo.Todo.Id,
        TodoDescription = todo.Todo.TodoDescription,
        TodoName = todo.Todo.TodoName,
        Status = todo.Todo.Status
      }).ToListAsync();
    }
  }
}