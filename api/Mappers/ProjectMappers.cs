using System;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class ProjectMappers
  {
    public static ProjectDto ToProjectDto(this Project projectModel)
    {
      return new ProjectDto
      {
        Id = projectModel.Id,
        ProjectName = projectModel.ProjectName,
        ProjectDescription = projectModel.ProjectDescription,
        Todos = projectModel.Todos.Select(s => s.ToTodoDto()).ToList(),
        Users = projectModel.ProjectProfiles.Select(p => p.AppUser.ToUserDto()).ToList(),
      };
    }


    public static Project ToProjectFromCreateDto(this CreateProjectRequestDto projectRequestDto)
    {
      return new Project {
        ProjectName = projectRequestDto.ProjectName,
        ProjectDescription = projectRequestDto.ProjectDescription,
      };
    }
  }
}