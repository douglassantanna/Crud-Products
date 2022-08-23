using products.Domain.Shared;

namespace products.Domain.Carts.Entities;
public class CartItem : Entity
{
    public CartItem(int userId, int itemId, int quantity)
    {
        UserId = userId;
        ItemId = itemId;
        Quantity = quantity;
    }

    public int UserId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }

}