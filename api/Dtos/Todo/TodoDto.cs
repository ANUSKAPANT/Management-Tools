using System;
using api.Dtos.Account;
using api.Models;
namespace api.Dtos
{
	public class TodoDto
	{
    public int Id { get; set; }
    public string TodoName { get; set; } = string.Empty;
    public string TodoDescription { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Todo;
    public int? ProjectId { get; set; }
    public List<UserDto> Users { get; set; } = [];
  }
}


