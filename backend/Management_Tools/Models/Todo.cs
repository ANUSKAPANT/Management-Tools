using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Management_Tools.Models
{
	public class Todo
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TodoName { get; set; }
        public string TodoDescription { get; set; }
        // Other task properties as needed

        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public List<User>? Users { get; set; }
    }
}

