using MaplrSugarShack.Services;
using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context, ICartItemRepository cartItemRepository)
        {
            _context = context;
            _cartItemRepository = cartItemRepository;
        }

        public void Add(Order order)
        {
            order.Date = DateTime.Now;
            order.Total = _cartItemRepository.GetTotal();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var cartItem in _cartItemRepository.GetAll())
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = cartItem.ProductId,
                    Count = cartItem.Count,
                    Price = cartItem.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
