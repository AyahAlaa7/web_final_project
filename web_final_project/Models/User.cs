using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace web_final_project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required, MaxLength(60)]
        public string Username { get; set; } = null!;


        [Required]
        public string Password { get; set; } = null!;


        [Required, EmailAddress]
        public string Email { get; set; } = null!;


        public Cart? Cart { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}