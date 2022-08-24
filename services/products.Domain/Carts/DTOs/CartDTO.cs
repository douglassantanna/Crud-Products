using products.Domain.Shared;

namespace products.Domain.Carts.DTOs;
public class CartDTO : Entity
{
    public CartHeaderDTO CartHeader { get; set; } = new CartHeaderDTO();
    public IEnumerable<CartItemDTO> Items { get; set; } = Enumerable.Empty<CartItemDTO>();
}
