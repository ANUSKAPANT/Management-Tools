using System;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class TodoMappers
  {
    public static TodoDto ToTodoDto(this Todo todoModel)
    {
      return new TodoDto
      {
        Id = todoModel.Id,
        TodoName = todoModel.TodoName,
        TodoDescription = todoModel.TodoDescription,
        Status = todoModel.Status,
        ProjectId = todoModel.ProjectId,
        Users = todoModel.TodoProfiles.Select(t => t.AppUser.ToUserDto()).ToList(),
      };
    }


    public static Todo ToTodoFromCreateDto(this CreateTodoRequestDto todoRequestDto, int? projectId)
    {
      if(projectId != null) {
        return new Todo {
          TodoName = todoRequestDto.TodoName,
          TodoDescription = todoRequestDto.TodoDescription,
          Status = todoRequestDto.Status,
          ProjectId = projectId,
        };
      }

      return new Todo {
        TodoName = todoRequestDto.TodoName,
        TodoDescription = todoRequestDto.TodoDescription,
        Status = todoRequestDto.Status,
      };
    }


    public static Todo ToTodoFromUpdateDto(this UpdateTodoRequestDto todoRequestDto)
    {
      return new Todo {
        Id = todoRequestDto.Id,
        TodoName = todoRequestDto.TodoName,
        TodoDescription = todoRequestDto.TodoDescription,
        Status = todoRequestDto.Status,
      };
    }
  }
}