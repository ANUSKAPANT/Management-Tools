using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface IProjectProfileRepository
  {
    Task<List<Project>> GetUserProjectProfile(AppUser user);
    Task<ProjectProfile> CreateAsync(ProjectProfile projectProfile);
    Task<ProjectProfile> DeleteProjectProfile(AppUser appUser, int projectId);
  }
}