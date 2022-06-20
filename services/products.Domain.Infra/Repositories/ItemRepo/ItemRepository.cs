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
        public async Task UpdateAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Item item)
        {
            _context.Itens.Remove(item);
            await _context.SaveChangesAsync();
        }
        public Item? GetById(int id) => _context.Itens.FirstOrDefault(x => x.Id == id);
    }
}