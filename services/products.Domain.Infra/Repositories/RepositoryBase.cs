using Microsoft.EntityFrameworkCore;
using products.Domain.Entities;
using products.Domain.Infra.Context;

namespace products.Domain.Infra.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
{
    public readonly DbSet<TEntity> _dbSet;
    public readonly AppDbContext _appDbContext;

    public RepositoryBase(AppDbContext appDbContext)
    {
        _dbSet = appDbContext.Set<TEntity>();
        _appDbContext = appDbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity item)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TEntity item)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
       return await _appDbContext.Itens.AsNoTracking().Where(ItemQueries.GetAll(item));
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(TEntity item)
    {
        throw new NotImplementedException();
    }
}