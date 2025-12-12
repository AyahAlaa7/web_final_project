namespace OnlineBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Pending, Confirmed, Shipped, Cancelled
        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}


