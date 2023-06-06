using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public class MockProductTypeRepository : IReadOnlyRepository<ProductType>
    {
        private List<ProductType> _productTypes;

        public MockProductTypeRepository()
        {
            _productTypes = new()
            {
                new () { Id = 1, Name = "Amber" },
                new () { Id = 2, Name = "Dark" },
                new () { Id = 3, Name = "Clear" }
            };

        }
        public List<ProductType> GetAll()
        {
            return _productTypes.ToList();
        }

        public ProductType? GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}
