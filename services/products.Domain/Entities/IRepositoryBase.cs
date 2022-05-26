using products.Domain.Entities;

namespace products.Domain.Entities;

public interface IRepositoryBase<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync(string name);

    Task<TEntity> GetByIdAsync(int id);

    Task<TEntity> CreateAsync(TEntity item);

    Task Update(TEntity item);

    Task DeleteAsync(TEntity item);
}
