using products.Domain.Carts.Entities;

namespace products.Domain.Carts.Contracts;

public interface ICartRepository
{
    Task<Cart> GetCartByUserID(int id);
    Task<Cart> UpdateCart(Cart cart);
    Task<bool> DeleteCartItem(int cartItemId);
    Task<bool> CleanCart(int userId);
    Task<bool> ApplyVoucher(int userId, string voucher);
    Task<bool> SeleteVoucher(int userId);
}
