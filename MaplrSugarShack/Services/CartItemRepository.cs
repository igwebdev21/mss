using MaplrSugarShack.Services;
using MaplrSugarSnack.Models;
using Microsoft.EntityFrameworkCore;

namespace MaplrSugarSnack.Services
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public string? CartId { get; set; }

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public static ICartItemRepository GetCart(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            var context = serviceProvider.GetRequiredService<AppDbContext>() ?? throw new Exception("Error initializing");
            return new CartItemRepository(context) { CartId = cartId };
        }

        public void Add(int productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId == CartId && x.ProductId == productId);
            if (cartItem == null)
            {
                _context.CartItems.Add(new CartItem { ProductId = productId, Count = 1, CartId = CartId });
            }
            else
            {
                cartItem.Count++;
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetAll()
        {
            var cartItems = _context.CartItems.Include(c => c.Product).Where(c => c.CartId == CartId);
            return cartItems.ToList();
        }

        public int GetCount()
        {
            return _context.CartItems.Where(c => c.CartId == CartId).Sum(c => c.Count);
        }

        public decimal GetTotal()
        {
            // Sqlite doesn't support sum of decimal values, so we will use Linq to Objects to sum
            return _context.CartItems.Where(c => c.CartId == CartId).Include(c => c.Product).Select(c => c.Product.Price * c.Count).ToList().Sum();
        }

        public void Remove(int productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.CartId == CartId && c.ProductId == productId);
            if (cartItem != null)
            {
                if (cartItem.Count == 1)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Count--;
                }
            }
            _context.SaveChanges();
        }

        public void Clear()
        {
            var cartItems = _context.CartItems.Where(c => c.CartId == CartId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
    }
}
