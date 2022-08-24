using products.Domain.Itens.DTOs;
using products.Domain.Shared;

namespace products.Domain.Carts.DTOs;
public class CartItemDTO : Entity
{
    public int Quantity { get; set; }
    public int ItemId { get; set; }
    public int CartHeaderId { get; set; }
    public ItemDTO Item { get; set; } = new();
}