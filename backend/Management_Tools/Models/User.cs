using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management_Tools.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
		[Required]
        [EmailAddress]
		public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        // Other user properties as needed
        public List<Project> Projects { get; set; }
        public List<Todo> Todos { get; set; }
    }
}

