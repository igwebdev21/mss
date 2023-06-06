using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public interface IWriteableRepository<TEntity> where TEntity: IEntity
    {
        void Add(TEntity enitity);
        void Update(TEntity enitity); 
        void Delete(int id);
    }
}
