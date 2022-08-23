using products.Domain.Shared;

namespace products.Domain.Carts.Entities;

public class Cart : Entity
{
    public Cart(int userId, List<CartItem> cartItens, bool isActive)
    {
        CartItens = cartItens;
        UserId = userId;
        IsActive = isActive;
    }
    public List<CartItem> CartItens { get; } = new List<CartItem>();
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }

}
