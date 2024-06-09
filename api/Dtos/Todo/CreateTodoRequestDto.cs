using System;
using System.ComponentModel.DataAnnotations;
using api.Models;
namespace api.Dtos
{
	public class CreateTodoRequestDto
	{
    [Required]
    public string TodoName { get; set; } = string.Empty;
    [Required]
    public string TodoDescription { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Todo;
    public List<String> UserIds { get; set; } = []; 
  }
}

