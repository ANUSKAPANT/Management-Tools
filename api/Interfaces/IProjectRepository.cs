using System;
using api.Dtos;
using api.Models;
namespace api.Interfaces
{
  public interface IProjectRepository
  {
    Task<List<Project>> GetProjectsAsync();
    Task<Project?> GetProjectAsync(int id);
    Task<Project?> CreateProjectAsync(CreateProjectRequestDto projectRequestDto);
    Task<Project?> UpdateProjectAsync(int id, UpdateProjectRequestDto projectdto);
    Task<Project?> DeleteProjectAsync(int id);
  }
}