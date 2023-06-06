using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class MockProductRepository : IReadOnlyRepository<Product>
    {
        private List<Product> _products;
        private readonly IReadOnlyRepository<ProductType> _productTypeRepository;

        public MockProductRepository(IReadOnlyRepository<ProductType> productTypeRepository)
        {
            _products = new() 
            {
                new () { Id = 1, Name = "Syrup 1", ProductTypeId = 1, Description = "Description 1", Price = 10.0m },
                new () { Id = 2, Name = "Syrup 2", ProductTypeId = 1, Description = "Description 2", Price = 20.0m },
                new () { Id = 3, Name = "Syrup 3", ProductTypeId = 2, Description = "Description 3", Price = 20.0m },
                new () { Id = 4, Name = "Syrup 4", ProductTypeId = 2, Description = "Description 4", Price = 20.0m },
                new () { Id = 5, Name = "Syrup 5", ProductTypeId = 3, Description = "Description 5", Price = 30.0m }
            };

            _productTypeRepository = productTypeRepository;
        }

        public List<Product> GetAll()
        {
            var products = _products.Join(_productTypeRepository.GetAll(), p => p.ProductTypeId, pt => pt.Id, (p, pt) => 
            new Product 
            { 
                Id = p.Id, Name = p.Name, ProductType = pt, ProductTypeId = pt.Id, Description = p.Description, Price = p.Price 
            });
            return products.ToList();
        }

        public Product? GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}
