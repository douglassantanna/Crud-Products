using products.Domain.Shared;

namespace products.Domain.Itens.DTOs;
public class ItemDTO : Entity
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
}