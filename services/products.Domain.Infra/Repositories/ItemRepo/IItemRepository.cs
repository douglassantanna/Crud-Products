using products.Domain.Entities;

namespace products.Domain.Infra.Repositories.ItemRepo;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();
    Task<Item> GetByIdAsync(int id);
    Task<Item> CreateAsync(Item item);
    Task<int> UpdateAsync(int id, Item item);
    Task<int> DeleteAsync(int id);
}
