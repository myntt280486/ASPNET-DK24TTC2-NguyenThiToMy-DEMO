using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanNuoc.Data;
using WebBanNuoc.Models;
using WebBanNuoc.ViewModels;

namespace WebBanNuoc.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "ShoppingCart";

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Checkout
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var user = await _userManager.GetUserAsync(User);
            var model = new CheckoutViewModel
            {
                ShippingAddress = user.Address ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                CartItems = cart,
                TotalAmount = cart.Sum(c => c.Total)
            };

            return View(model);
        }

        // POST: Orders/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var cart = GetCart();
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    TotalAmount = cart.Sum(c => c.Total),
                    Status = OrderStatus.Pending,
                    ShippingAddress = model.ShippingAddress,
                    PhoneNumber = model.PhoneNumber,
                    PaymentMethod = model.PaymentMethod
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        SelectedOptions = item.SelectedOptions
                    };
                    _context.OrderDetails.Add(orderDetail);
                }

                await _context.SaveChangesAsync();

                // Clear cart
                HttpContext.Session.Remove(CartSessionKey);

                return RedirectToAction(nameof(Confirmation), new { id = order.Id });
            }

            model.CartItems = cart;
            model.TotalAmount = cart.Sum(c => c.Total);
            return View(model);
        }

        // GET: Orders/Confirmation/5
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private List<CartItemViewModel> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItemViewModel>();
            }
            return JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartJson) ?? new List<CartItemViewModel>();
        }
    }
}
