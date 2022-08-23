using Microsoft.EntityFrameworkCore;
using products.Domain.Auth.Entities;
using products.Domain.Infra.Context;
using BC = BCrypt.Net.BCrypt;

namespace products.Domain.Infra.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync() => await _db.Users.ToListAsync();

    public User GetByEmail(string email) => _db.Users.FirstOrDefault(e => e.Email == email);

    public User GetById(int id) => _db.Users.FirstOrDefault(x => x.Id == id);

    public string HashPassword(string password)
    {
        var hash = BC.HashPassword(password);
        return hash;
    }

    public async Task UpdateAsync(User user)
    {
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
}
