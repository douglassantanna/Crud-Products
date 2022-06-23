using Microsoft.EntityFrameworkCore;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Interfaces;
using products.Domain.Infra.Context;
using products.Domain.Shared;

namespace products.Domain.Infra.Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<NotificationResult> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null) return new NotificationResult("Cliente não encontrado", false);
            return new NotificationResult("", true, customer);
        }
        public async Task UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        public Customer? GetById(int id) => _context.Customers.FirstOrDefault(x => x.Id == id);
    }
}