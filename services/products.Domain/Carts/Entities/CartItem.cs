using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Carts.Entities;
public class CartItem : Entity
{

    public int Quantity { get; set; }
    public int ItemId { get; set; }
    public int CartHeaderId { get; set; }
    public Item Item { get; set; }
    public CartHeader CartHeader { get; set; } = new CartHeader();


}