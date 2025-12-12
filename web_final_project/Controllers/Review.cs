using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    internal class Review : Reviews
    {
        public int BookId { get; set; }
        public int AppUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}