using Microsoft.EntityFrameworkCore;
using products.Domain.Customers.DTOs;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Interfaces;
using products.Domain.Infra.Context;

namespace products.Domain.Infra.Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
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
        public Customer GetById(int id) => _context.Customers.Include(x => x.EnderecoEntrega).FirstOrDefault(x => x.Id == id);

        public bool EmailExists(string email)
        {
            var result = _context.Customers.Any(x => x.Email == email);
            return result;
        }

        public int UnderAge(DateTime date)
        {
            var age = 0;
            age = DateTime.Now.AddYears(-date.Year).Year;
            return age;
        }

        public Customer GetByCnpj_cpf(string document) => _context.Customers.FirstOrDefault(x => x.Cnpj_cpf == document);

        public dynamic GetCustomerWithAddress(int id) => _context.Customers.Include(x => x.EnderecoEntrega).Select(ViewCustomerExtension.ToView()).FirstOrDefault(x => x.Id == id);
    }
}