using Microsoft.EntityFrameworkCore;
using products.Domain.Carts.Contracts;
using products.Domain.Carts.Entities;
using products.Domain.Infra.Context;

namespace products.Domain.Infra.Repositories;
public class CartRepository : ICartRepository
{
    private readonly AppDbContext _db;
    private readonly IUserRepository _userRepository;
    public CartRepository(AppDbContext db, IUserRepository userRepository)
    {
        _db = db;
        _userRepository = userRepository;
    }

    public Task<bool> ApplyVoucher(int userId, string voucher)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CleanCart(int userId)
    {
        var cartHeader = await _db.CartHeader.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cartHeader is not null)
        {
            _db.CartItem.RemoveRange(_db.CartItem.Where(c => c.CartHeaderId == cartHeader.Id));
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteCartItem(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _db.CartItem.FirstOrDefaultAsync(c => c.Id == cartItemId);
            var total = _db.CartItem.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();
            _db.CartItem.Remove(cartItem);

            if (total == 1)
            {
                var cartHeaderRemove = await _db.CartHeader.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);
                _db.CartHeader.Remove(cartHeaderRemove);
            }
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Cart> GetCartByUserID(int id)
    {
        Cart cart = new Cart
        {
            CartHeader = await _db.CartHeader.FirstOrDefaultAsync(c => c.UserId == id)
        };
        cart.Items = _db.CartItem.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(i => i.Item);
        return cart;
    }

    public Task<bool> SeleteVoucher(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> UpdateCart(Cart cart)
    {
        // await SaveProductInDatabase(cart);
    }
    //     private async Task SaveProductInDatabase(Cart cart)
    //     {
    // var item = await _db.Itens.FirstOrDefaultAsync(i=>i.Id==cart.car)
    //     }
}
