using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
	public class Project
	{
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; } = String.Empty;
        [Required]
        public string ProjectDescription { get; set; } = String.Empty;
        public List<Todo> Todos { get; set; } = [];
        public List<ProjectProfile> ProjectProfiles { get; set; } = [];
    }
}
