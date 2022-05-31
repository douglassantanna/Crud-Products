using Microsoft.EntityFrameworkCore;
using products.Domain.Entities;
using products.Domain.Infra.Context;
using products.Domain.Infra.ViewModels.Item;
using products.Domain.Notifications;

namespace products.Domain.Infra.Repositories.ItemRepo
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationResult> CreateAsync(NewItem item)
        {
            var newItem = new Item(item.Name, item.Price);
            await _context.Itens.AddAsync(newItem);
            await _context.SaveChangesAsync();
            return new NotificationResult("", true, newItem);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var deletedItem = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
            _context.Itens.Remove(deletedItem);
            await _context.SaveChangesAsync();
            return deletedItem.Id;
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Itens.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, Item item)
        {
            var updateItem = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
            updateItem.UpdateName(item.Name);
            updateItem.UpdatePrice(item.Price);

            _context.Entry(updateItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateItem.Id;
        }
    }
}