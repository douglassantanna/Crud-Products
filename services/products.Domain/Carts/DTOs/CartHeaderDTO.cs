using products.Domain.Shared;

namespace products.Domain.Carts.DTOs;
public class CartHeaderDTO : Entity
{
    public int UserId { get; set; }
    public string Voucer { get; set; } = string.Empty;
}