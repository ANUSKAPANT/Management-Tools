using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;
using api.Dtos.Account;

namespace api.Dtos
{
	public class CreateProjectRequestDto
	{
    [Required]
    public string ProjectName { get; set; } = String.Empty;
    [Required]
    public string ProjectDescription { get; set; } = String.Empty;
    public List<CreateTodoRequestDto> Todos { get; set; } = [];
    public List<String> UserIds { get; set; } = []; 
  }
}
