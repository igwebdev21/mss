using MaplrSugarShack.Services;
using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class ProductTypeRepository : IReadOnlyRepository<ProductType>
    {
        private readonly AppDbContext _context;

        public ProductTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<ProductType> GetAll()
        {
            return _context.ProductTypes.ToList();
        }

        public ProductType? GetById(int id)
        {
            return _context.ProductTypes.FirstOrDefault(p => p.Id == id);
        }
    }
}
