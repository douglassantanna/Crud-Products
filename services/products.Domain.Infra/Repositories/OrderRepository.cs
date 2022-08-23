using Microsoft.EntityFrameworkCore;
using products.Domain.Infra.Context;
using products.Domain.Orders.Entities;
using products.Domain.Orders.Contracts;
using products.Domain.Shared;

namespace products.Domain.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<NotificationResult> GetByIdAsync(int id)
    {
        var order = await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
        if (order == null) return new NotificationResult("Pedido não encontrado", false);
        return new NotificationResult("", true, order);
    }
    public async Task UpdateAsync(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    public Order? GetById(int id) => _context.Orders.FirstOrDefault(x => x.Id == id);
}
