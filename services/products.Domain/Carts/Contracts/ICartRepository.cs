using products.Domain.Carts.DTOs;

namespace products.Domain.Carts.Contracts;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserID(int id);
    Task<CartDTO> UpdateCart(CartDTO cart);
    Task<bool> DeleteCartItem(int cartItemId);
    Task<bool> CleanCart(int userId);
    Task<bool> ApplyVoucher(int userId, string voucher);
    Task<bool> SeleteVoucher(int userId);
}
