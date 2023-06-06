using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public interface ICartItemRepository
    {
        void Clear();
        List<CartItem> GetAll();
        void Add(int productId);
        void Remove(int productId);
        decimal GetTotal();
        int GetCount();
    }
}
