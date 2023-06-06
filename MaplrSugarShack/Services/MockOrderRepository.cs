using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class MockOrderRepository : IOrderRepository
    {
        private readonly ICartItemRepository _cartItemRepository;
        private List<Order> _orders;
        
        public MockOrderRepository(ICartItemRepository cartItemRepository) 
        { 
            _orders = new List<Order>();
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
            _orders.Add(order);
        }
    }
}
