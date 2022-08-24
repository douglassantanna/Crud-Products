using Microsoft.EntityFrameworkCore;
using products.Domain.Auth.Contracts;
using products.Domain.Auth.Entities;
using products.Domain.Infra.Context;
using BC = BCrypt.Net.BCrypt;

namespace products.Domain.Infra.Repositories;
public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _db;

    public AuthRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Authenticate(User user)
    {
        var account = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (account is null) return false;
        return BC.Verify(user.Password, account.Password);
    }
}