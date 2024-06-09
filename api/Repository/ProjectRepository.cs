using System;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Dtos;
using api.Mappers;
using api.Dtos.Account;
using Microsoft.AspNetCore.Identity;
namespace api.Repository
{
  public class ProjectRepository : IProjectRepository
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ProjectRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }
    public async Task<Project?> CreateProjectAsync(CreateProjectRequestDto projectRequestDto)
    {
      var todos = projectRequestDto.Todos.Select(t => t.ToTodoFromCreateDto(null)).ToList();
      var userIds = projectRequestDto.UserIds;
      var project = projectRequestDto.ToProjectFromCreateDto();
      using var transaction = _context.Database.BeginTransaction();

      try
      {
        project.Todos.AddRange(todos);
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        foreach (string id in userIds)
        {
          if (!project.ProjectProfiles.Any(p => p.AppUserId == id))
          {
            var appUser = await _userManager.FindByIdAsync(id);
            if(appUser == null) return null;
            var projectProfile = new ProjectProfile { ProjectId = project.Id, AppUserId = appUser.Id };
            await _context.ProjectProfiles.AddAsync(projectProfile);
          }
        }
        await _context.SaveChangesAsync();
        transaction.Commit();
      }
      catch (Exception)
      {
        return null;
      }

      return project;
    }

    public async Task<Project?> DeleteProjectAsync(int id)
    {
      var project = await _context.Projects.Include(p => p.Todos).Include(p => p.ProjectProfiles).FirstOrDefaultAsync(p => p.Id == id);
      if (project == null) return null;
      _context.Remove(project);
      await _context.SaveChangesAsync();
      return project;
    }

    public async Task<Project?> GetProjectAsync(int id)
    {
      var project = await _context.Projects.Include(p => p.Todos).ThenInclude(p => p.TodoProfiles).ThenInclude(t => t.AppUser).Include(p => p.ProjectProfiles).ThenInclude(p => p.AppUser).FirstOrDefaultAsync(p => p.Id == id);
      return project;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
      var projects = await _context.Projects.Include(p => p.Todos).ThenInclude(p => p.TodoProfiles).ThenInclude(t => t.AppUser).Include(p => p.ProjectProfiles).ThenInclude(p => p.AppUser).ToListAsync();
      return projects;
    }

    public async Task<Project?> UpdateProjectAsync(int id, UpdateProjectRequestDto projectdto)
    {
      var project = await _context.Projects.Include(p => p.Todos).Include(p => p.ProjectProfiles).FirstOrDefaultAsync(p => p.Id == id);
      if (project == null) return null;

      // updating project profile
      var userIds = projectdto.UserIds;
    
      project.ProjectProfiles.RemoveAll(t => !userIds.Contains(t.AppUserId));

      var newUserIds = userIds.Where(id => !project.ProjectProfiles.Any(t => t.AppUserId == id)).ToList();

      foreach(string userId in newUserIds)
      {
        var appUser = await _userManager.FindByIdAsync(userId);
        if(appUser == null) continue;
        var projectProfile = new ProjectProfile { ProjectId = project.Id, AppUserId = appUser.Id };
        project.ProjectProfiles.Add(projectProfile);
      };

      project.Todos.RemoveAll(t => !projectdto.Todos.Any(p => p.Id == t.Id));

      var todosFromDatabaseIds = project.Todos.Select(t => t.Id).ToList();

      var addTodos = projectdto.Todos
        .Where(todo => !todosFromDatabaseIds.Contains(todo.Id))
        .Select(t => t.ToTodoFromUpdateDto()) // Transform todos
        .ToList();
      
      project.Todos.AddRange(addTodos);

      project.ProjectDescription = projectdto.ProjectDescription;
      project.ProjectName = projectdto.ProjectName;

      await _context.SaveChangesAsync();
      return project;

    }
  }
}