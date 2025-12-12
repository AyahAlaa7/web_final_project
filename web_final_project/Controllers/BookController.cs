using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;

namespace OnlineBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId, string? sort)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) || b.Author.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }

            query = sort switch
            {
                "price_asc" => query.OrderBy(b => b.Price),
                "price_desc" => query.OrderByDescending(b => b.Price),
                "popular" => query.OrderByDescending(b => b.PopularityScore),
                _ => query.OrderBy(b => b.Title)
            };

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.Search = search;
            ViewBag.Sort = sort;

            var books = await query.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.AppUser)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }
    }
}




