using products.Domain.Orders.Entities;
using products.Domain.Shared;

namespace products.Domain.Orders.Interfaces;
public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<NotificationResult> GetByIdAsync(int id);
    Task CreateAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
    Order? GetById(int id);
}