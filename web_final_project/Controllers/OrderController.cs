using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;
using System.Security.Claims;

namespace OnlineBookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = GetUserId();
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .Where(o => o.AppUserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();
            var cartItems = await _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.AppUserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                AppUserId = userId,
                Status = "Pending",
                OrderDate = DateTime.UtcNow
            };

            foreach (var ci in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Book!.Price
                });
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyOrders");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetUserId();
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.AppUserId == userId);
            if (order == null) return NotFound();

            if (order.Status == "Pending")
            {
                order.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyOrders");
        }
    }
}



