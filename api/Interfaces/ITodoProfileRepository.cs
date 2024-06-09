using System;
using api.Models;

namespace api.Interfaces
{
  public interface ITodoProfileRepository
  {
    Task<List<Todo>> GetUserTodoProfile(AppUser user);
    Task<TodoProfile> CreateAsync(TodoProfile todoProfile);
    Task<TodoProfile> DeleteTodoProfile(AppUser appUser, int todoId);
  }
}