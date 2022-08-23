using products.Domain.Auth.Entities;

namespace products.Domain.Auth.Contracts;
public interface IAuthRepository
{
    Task<bool> Authenticate(User user);
}