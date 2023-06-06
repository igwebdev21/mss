using MaplrSugarShack.Services;
using MaplrSugarSnack.Models;
using Microsoft.EntityFrameworkCore;

namespace MaplrSugarSnack.Services
{
    public class ProductRepository : IReadOnlyRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(p => p.ProductType).ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.Include(p => p.ProductType).FirstOrDefault(p => p.Id == id);
        }
    }
}
