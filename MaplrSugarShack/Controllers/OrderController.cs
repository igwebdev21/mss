using MaplrSugarSnack.Models;
using MaplrSugarSnack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public OrderController(IOrderRepository orderRepository, ICartItemRepository cartItemRepository)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
        }

        public IActionResult Checkout()
        {
            ViewBag.Title = "Checkout";
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                _cartItemRepository.Clear();
                return RedirectToAction("CheckoutComplete");
            }
            return View();
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Order complete! Thank you for your order!";
            return View();
        }
    }
}
