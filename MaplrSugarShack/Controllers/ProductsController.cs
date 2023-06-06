using MaplrSugarSnack.Models;
using MaplrSugarSnack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IReadOnlyRepository<Product> _productRepository;

        public ProductsController(IReadOnlyRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(int? productTypeId)
        {
            ViewBag.Title = "Syrups";
            var products = _productRepository.GetAll();
            if (productTypeId != null)
            {
                products = products.Where(p => p.ProductTypeId == productTypeId).ToList();
            }
            return View(products);
        }

        public IActionResult Details(int id) 
        {
            ViewBag.Title = "Details";
            var product = _productRepository.GetById(id);
            if (product == null) 
            { 
                return NotFound();
            }
            return View(product);
        }
    }
}
