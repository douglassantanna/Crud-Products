using products.Domain.Shared;

namespace products.Domain.Itens.Entities;

public class Item : Entity
{
    public Item(string name, decimal price)
    {
        Name = name;
        Price = price;
        CreatedAt = DateTime.UtcNow;
    }
    protected Item() { }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedWhen { get; private set; }
    public void UpdateName(string name) => Name = name;
    public void UpdatePrice(decimal price) => Price = price;
    public void UpdatedAt() => UpdatedWhen = DateTime.UtcNow;
}
