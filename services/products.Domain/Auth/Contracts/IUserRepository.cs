using products.Domain.Auth.Entities;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    User GetById(int id);
    User GetByEmail(string email);
    string HashPassword(string password);
}