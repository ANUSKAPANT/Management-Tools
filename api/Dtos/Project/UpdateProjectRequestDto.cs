using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Dtos
{
	public class UpdateProjectRequestDto
	{
    [Required]
    public string ProjectName { get; set; } = String.Empty;
    [Required]
    public string ProjectDescription { get; set; } = String.Empty;
    public List<UpdateTodoRequestDto> Todos { get; set; } = [];
    public List<String> UserIds { get; set; } = []; 
  }
}
