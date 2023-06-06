using MaplrSugarSnack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Components
{
    public class CartSummary: ViewComponent
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartSummary(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cartItemRepository);
        }
    }
}
