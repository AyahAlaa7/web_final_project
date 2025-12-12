using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminBookController : Controller
    {
        private readonly AppDbContext _context;

        public AdminBookController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Category).ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(book);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(book);
            }

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
