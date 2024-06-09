using Microsoft.AspNetCore.Identity;

namespace api.Models
{
  public class AppUser : IdentityUser
  {
    public List<ProjectProfile> ProjectProfiles { get; set; } = [];
    public List<TodoProfile> TodoProfiles { get; set; } = [];
  }
}

