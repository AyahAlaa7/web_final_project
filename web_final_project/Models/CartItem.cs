namespace OnlineBookStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int Quantity { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}


