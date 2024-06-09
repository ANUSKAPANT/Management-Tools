using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models
{
  [Table("TodoProfiles")]
  public class TodoProfile
  {
    public int TodoId { get; set; }
    public string? AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public Todo? Todo { get; set; }
  }
}