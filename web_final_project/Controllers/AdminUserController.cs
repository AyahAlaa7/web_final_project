using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;

namespace OnlineBookStore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminUserController : Controller
    {
        private readonly AppDbContext _context;

        public AdminUserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // FIX: Use AppUsers instead of Users
            return View(_context.AppUsers.ToList());
        }

        public IActionResult MakeAdmin(int id)
        {
            // FIX: Use AppUsers instead of Users
            var user = _context.AppUsers.Find(id);

            if (user != null)
            {
                user.Role = "Admin";
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

