using products.Domain.Carts.Entities;

namespace products.Domain.Carts.Interfaces;

public interface ICartRepository
{
    Task<Cart> AddItemToCart(int userId, int itemId, int quantity);
    // Task UpdateAsync(Cart cart);
    // Task DeleteAsync(Cart cart);
}
