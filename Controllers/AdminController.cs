using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.ViewModels;

namespace MusicStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly MusicStoreContext _context;

        public AdminController(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();

            AdminDashboardViewModel viewModel = new AdminDashboardViewModel
            {
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                PendingOrders = orders.Count(o => o.Status == "Pending"),

                TotalCustomers = await _context.Orders
                    .Select(o => o.CustomerEmail)
                    .Distinct()
                    .CountAsync(),

                RecentOrders = orders
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Orders(string? status)
        {
            var allOrders = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                allOrders = allOrders.Where(o => o.Status == status);
            }

            var orderList = await allOrders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.Status = status;

            return View(orderList);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Album)
                .ThenInclude(a => a.Artist)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
