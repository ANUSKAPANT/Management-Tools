using api.Models;

namespace api.Dtos.Account
{
  public class UserDto
  {
    public string Id { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = string.Empty;
    public List<ProjectProfile> ProjectProfiles { get; set; } = [];
    public List<TodoProfile> TodoProfiles { get; set; } = [];
  }
}