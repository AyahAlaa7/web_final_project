using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;
using OnlineBookStore.Models;
using System.Security.Claims;

namespace OnlineBookStore.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(int bookId, int rating, string comment)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = new Review
            {
                BookId = bookId,
                AppUserId = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Book", new { id = bookId });
        }
    }
}