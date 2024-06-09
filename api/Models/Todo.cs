using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    [Table("Todos")]
	public class Todo
	{
        public int Id { get; set; }
        public string TodoName { get; set; } = string.Empty;
        public string TodoDescription { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Todo;
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public List<TodoProfile> TodoProfiles { get; set; } = [];
    }

    public enum Status
    {
        Todo,
        InProgress,
        InReview,
        Complete
    }
}

