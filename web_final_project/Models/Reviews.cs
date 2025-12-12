using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}


