using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Management_Tools.Models;

namespace Management_Tools.Models
{
	public class Project
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        // Other project properties as needed
        public List<User> Users { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
