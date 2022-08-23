using Microsoft.EntityFrameworkCore;
using products.Domain.Carts.Contracts;
using products.Domain.Carts.Entities;
using products.Domain.Infra.Context;

namespace products.Domain.Infra.Repositories;
public class CartRepository : ICartRepository
{
    private readonly AppDbContext _db;
    public CartRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Cart> AddItemToCart(int userId, int itemId, int quantity)
    {
        var cart = _db.Carts
                    .Where(x => x.IsActive == true && x.UserId == userId)
                    .Include(y => y.CartItens)
                    .FirstOrDefault();

        if (cart is not null)
        {
            if (cart.CartItens.Any(i => i.Id != itemId))
                cart.CartItens.Add(new CartItem(userId, itemId, quantity));

            else
                cart.CartItens.Where(c => c.ItemId == itemId).FirstOrDefault().Quantity += quantity;
        }
        else
            _db.Add(new Cart(userId, new List<CartItem>() { new CartItem(userId, itemId, quantity) }, true));

        await _db.SaveChangesAsync();
        return _db.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefault();
    }
}