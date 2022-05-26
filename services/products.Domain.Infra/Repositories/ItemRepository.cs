using products.Domain.Entities;
using products.Domain.Infra.Context;
using products.Domain.Infra.Repositories.Abstractions;

namespace products.Domain.Infra.Repositories;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(AppDbContext appContext) : base(appContext)
    {
    }
}