using products.Domain.Entities;
using products.Domain.Infra.ViewModels.Item;
using products.Domain.Notifications;

namespace products.Domain.Infra.Repositories.ItemRepo;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();
    Task<NotificationResult> GetByIdAsync(int id);
    Task<NotificationResult> CreateAsync(NewItem item);
    Task<NotificationResult> UpdateAsync(int id, Item item);
    Task<NotificationResult> DeleteAsync(int id);
}
