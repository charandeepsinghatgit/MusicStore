using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class CartController : Controller
    {
        private readonly MusicStoreContext _context;

        public CartController(MusicStoreContext context)
        {
            _context = context;
        }

        // Get or create session ID for the cart
        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString("CartSessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartSessionId", sessionId);
            }
            return sessionId;
        }

        // Get or create cart for current session
        private async Task<Cart> GetOrCreateCart()
        {
            var sessionId = GetSessionId();
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Album)
                .ThenInclude(a => a.Artist)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);

            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        // GET: /Cart - View cart contents
        public async Task<IActionResult> Index()
        {
            var cart = await GetOrCreateCart();
            return View(cart);
        }

        // GET: /Cart/Add/1 - Add album to cart
        public async Task<IActionResult> Add(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            var cart = await GetOrCreateCart();

            // Check if album already exists in cart
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.AlbumId == id);

            if (existingItem != null)
            {
                // Update quantity if item already exists
                existingItem.Quantity++;
            }
            else
            {
                // Add new item to cart
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    AlbumId = id,
                    Quantity = 1,
                    AddedDate = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            cart.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"{album.Title} has been added to your cart!";
            return RedirectToAction("Index");
        }

        // GET: /Cart/Update/1?quantity=2 - Update item quantity
        public async Task<IActionResult> Update(int id, int quantity)
        {
            var cart = await GetOrCreateCart();
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (quantity > 0)
            {
                cartItem.Quantity = quantity;
                cart.LastUpdated = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                // If quantity is 0 or less, remove the item
                return await Remove(id);
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/Remove/1 - Remove item from cart
        public async Task<IActionResult> Remove(int id)
        {
            var cart = await GetOrCreateCart();
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            cart.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Helper action to get cart item count (for displaying in navigation)
        public async Task<int> GetCartItemCount()
        {
            var sessionId = GetSessionId();
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);

            return cart?.CartItems.Sum(ci => ci.Quantity) ?? 0;
        }
    }
}
