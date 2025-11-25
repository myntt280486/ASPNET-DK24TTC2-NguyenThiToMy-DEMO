using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanNuoc.Data;
using WebBanNuoc.Models;

namespace WebBanNuoc.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, int rating, string? comment)
        {
            var user = await _userManager.GetUserAsync(User);
            
            // Check if user has purchased this product
            var hasPurchased = await _context.OrderDetails
                .Include(od => od.Order)
                .AnyAsync(od => od.ProductId == productId && od.Order.UserId == user.Id);

            if (!hasPurchased)
            {
                TempData["Error"] = "Bạn chỉ có thể đánh giá sản phẩm đã mua.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            // Check if user already reviewed this product
            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == user.Id);

            if (existingReview != null)
            {
                TempData["Error"] = "Bạn đã đánh giá sản phẩm này rồi.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var review = new Review
            {
                ProductId = productId,
                UserId = user.Id,
                Rating = rating,
                Comment = comment,
                CreatedDate = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đánh giá của bạn đã được gửi thành công!";
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
