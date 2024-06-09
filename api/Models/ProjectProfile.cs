using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models
{
  [Table("ProjectProfiles")]
  public class ProjectProfile
  {
    public int ProjectId { get; set; }
    public string AppUserId { get; set; }

    public AppUser? AppUser { get; set; } 

    public Project? Project { get; set; }
  }
}