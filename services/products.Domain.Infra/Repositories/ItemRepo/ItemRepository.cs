using Microsoft.EntityFrameworkCore;
using products.Domain.Infra.Context;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Infra.Repositories.ItemRepo
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Item item)
        {
            await _context.Itens.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Itens.ToListAsync();
        }

        public async Task<NotificationResult> GetByIdAsync(int id)
        {
            var item = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return new NotificationResult("Item não encontrado", false);
            return new NotificationResult("", true, item);
        }
        public async Task<NotificationResult> UpdateAsync(int id, Item item)
        {
            var updateItem = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
            if (updateItem == null) return new NotificationResult("Item não encontrado", false);
            updateItem.UpdateName(item.Name);
            updateItem.UpdatePrice(item.Price);
            updateItem.UpdatedAt();
            _context.Entry(updateItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new NotificationResult("Item alterado com sucesso!", true);
        }
        public async Task<NotificationResult> DeleteAsync(int id)
        {
            var deletedItem = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
            if (deletedItem == null) return new NotificationResult("Item não encontrado", false);
            _context.Itens.Remove(deletedItem);
            await _context.SaveChangesAsync();
            return new NotificationResult("Item excluído com sucesso!", true);
        }
    }
}