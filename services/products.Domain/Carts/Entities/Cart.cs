using products.Domain.Shared;

namespace products.Domain.Carts.Entities;

public class Cart : Entity
{
    public CartHeader CartHeader { get; set; } = new CartHeader();
    public IEnumerable<CartItem> Items { get; set; } = Enumerable.Empty<CartItem>();
}
