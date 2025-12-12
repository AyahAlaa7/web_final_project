using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdminCategory
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // GET: AdminCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCategory/Create
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: AdminCategory/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: AdminCategory/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


