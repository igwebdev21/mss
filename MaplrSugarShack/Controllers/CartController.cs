using MaplrSugarSnack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartController(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Cart";
            return View(_cartItemRepository);
        }

        public IActionResult Add(int productId) 
        {
            _cartItemRepository.Add(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId) 
        {
            _cartItemRepository.Remove(productId);
            return RedirectToAction("Index");

        }
    }
}
