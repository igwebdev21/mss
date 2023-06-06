using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class MockCartItemRepository : ICartItemRepository
    {
        private readonly IReadOnlyRepository<Product> _productRepository;
        public string CartId { get; set; }
        List<CartItem> _cartItems;

        public MockCartItemRepository(IReadOnlyRepository<Product> productRepository)
        {
            CartId = Guid.NewGuid().ToString();
            _cartItems = new();
            _productRepository = productRepository;
        }

        public static ICartItemRepository GetCart(IServiceProvider serviceProvider)
        {
            var productRepository = serviceProvider.GetService<IReadOnlyRepository<Product>>() ?? throw new Exception("Error initializing");
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new MockCartItemRepository(productRepository) { CartId = cartId };
        }

        public void Add(int productId)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.CartId == CartId && x.ProductId == productId);
            if (cartItem == null)
            {
                _cartItems.Add(new CartItem { ProductId = productId, Count = 1, CartId = CartId });
            }
            else
            {
                cartItem.Count++;
            }
        }

        public List<CartItem> GetAll()
        {
            var cartItems = _cartItems.Join(_productRepository.GetAll(), c=>c.ProductId, p=>p.Id, (c,p)=> 
            new CartItem
            {
                Id = c.Id,
                CartId = c.CartId,
                ProductId = c.ProductId,
                Product = p,
                Count = c.Count
            });
            return cartItems.ToList();
        }

        public int GetCount()
        {
            return _cartItems.Sum(c => c.Count);
        }

        public decimal GetTotal()
        {
            return GetAll().Sum(c => c.Product.Price * c.Count);
        }

        public void Remove(int productId)
        {
            var cartItem = _cartItems.FirstOrDefault(c => c.CartId == CartId && c.ProductId == productId);
            if (cartItem != null)
            {
                if (cartItem.Count == 1)
                {
                    _cartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Count--;
                }
            }
        }

        public void Clear()
        {
            _cartItems.Clear();
        }
    }
}
