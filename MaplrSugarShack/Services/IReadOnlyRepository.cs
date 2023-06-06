using MaplrSugarSnack.Models;

namespace MaplrSugarSnack.Services
{
    public interface IReadOnlyRepository<TEntity> where TEntity: IEntity
    {
        List<TEntity> GetAll();
        TEntity? GetById(int id);
    }
}
