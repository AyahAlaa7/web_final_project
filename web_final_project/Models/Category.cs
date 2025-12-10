using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace web_final_project.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }


        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;


        public string? Description { get; set; }


        public ICollection<Book>? Books { get; set; }
    }
}

