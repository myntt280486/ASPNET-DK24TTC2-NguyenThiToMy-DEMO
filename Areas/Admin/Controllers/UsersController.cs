using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanNuoc.Data;
using WebBanNuoc.Models;

namespace WebBanNuoc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.UserId == id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.Orders = orders;
            ViewBag.TotalOrders = orders.Count;
            ViewBag.TotalSpent = orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalAmount);

            return View(user);
        }

        // POST: Admin/Users/ToggleLock/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (await _userManager.GetLockoutEndDateAsync(user) != null && await _userManager.GetLockoutEndDateAsync(user) > DateTimeOffset.Now)
            {
                // Unlock user
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            else
            {
                // Lock user
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
