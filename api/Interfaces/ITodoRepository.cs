using System;
using api.Dtos;
using api.Models;
namespace api.Interfaces
{
  public interface ITodoRepository
  {
    Task<List<Todo>> GetTodosAsync();
    Task<Todo?> GetTodoAsync(int id);
    Task<Todo?> CreateTodoAsync(CreateTodoRequestDto todo, int projectId);
    Task<Todo?> UpdateTodoAsync(int id, UpdateTodoRequestDto tododto);
    Task<Todo?> DeleteTodoAsync(int id);
  }
}