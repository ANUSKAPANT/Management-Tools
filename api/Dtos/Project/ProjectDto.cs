using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Dtos.Account;
using api.Models;

namespace api.Dtos
{
	public class ProjectDto
	{
    public int Id { get; set; }
    [Required]
    public string ProjectName { get; set; } = String.Empty;
    [Required]
    public string ProjectDescription { get; set; } = String.Empty;
    public List<TodoDto> Todos { get; set; } = new List<TodoDto>();
    public List<UserDto> Users { get; set; } = [];
  }
}
