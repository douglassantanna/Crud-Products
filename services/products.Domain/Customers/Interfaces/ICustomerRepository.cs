using products.Domain.Customers.Entities;
using products.Domain.Shared;

namespace products.Domain.Customers.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<NotificationResult> GetByIdAsync(int id);
    Task CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    Customer? GetById(int id);
    bool EmailExists(string email);
}
