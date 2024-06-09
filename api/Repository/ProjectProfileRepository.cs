using System;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class ProjectProfileRepository : IProjectProfileRepository
  {
    private readonly ApplicationDbContext _context;
    public ProjectProfileRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<ProjectProfile> CreateAsync(ProjectProfile projectProfile)
    {
      await _context.ProjectProfiles.AddAsync(projectProfile);
      await _context.SaveChangesAsync();
      return projectProfile;
    }

    public async Task<ProjectProfile> DeleteProjectProfile(AppUser appUser, int projectId)
    {
      var projectProfileModel = await _context.ProjectProfiles.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Project.Id == projectId);

      if (projectProfileModel == null)
      {
        return null;
      }

      _context.ProjectProfiles.Remove(projectProfileModel);
      await _context.SaveChangesAsync();
      return projectProfileModel;
    }

    public async Task<List<Project>> GetUserProjectProfile(AppUser user)
    {
      return await _context.ProjectProfiles.Where(u => u.AppUserId == user.Id)
      .Select(project => new Project
      {
        Id = project.Project.Id,
        ProjectDescription = project.Project.ProjectDescription,
        ProjectName = project.Project.ProjectName,
        Todos = project.Project.Todos
      }).ToListAsync();
    }
  }
}