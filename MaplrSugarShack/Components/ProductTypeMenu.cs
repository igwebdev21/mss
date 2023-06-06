using MaplrSugarSnack.Models;
using MaplrSugarSnack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Components
{
    public class ProductTypeMenu: ViewComponent
    {
        private readonly IReadOnlyRepository<ProductType> _productTypeRepository;

        public ProductTypeMenu(IReadOnlyRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productTypeRepository.GetAll());

        }
    }
}
