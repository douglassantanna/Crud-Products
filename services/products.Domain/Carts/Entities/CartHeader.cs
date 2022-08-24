using products.Domain.Shared;

namespace products.Domain.Carts.Entities;
public class CartHeader : Entity
{
    public int UserId { get; set; }
    public string Voucer { get; set; } = string.Empty;

}