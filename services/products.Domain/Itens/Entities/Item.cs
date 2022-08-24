using products.Domain.Shared;

namespace products.Domain.Itens.Entities;

public class Item : Entity
{
    public Item(string name, decimal price, Category category)
    {
        Name = name;
        Price = price;
        CreatedAt = DateTime.UtcNow;
        Category = category;
    }
    protected Item() { }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Category Category { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedWhen { get; private set; }
    public void UpdateItem(string name, decimal price, Category category)
    {
        Name = name;
        Price = price;
        Category = category;
    }
    public void UpdatedAt() => UpdatedWhen = DateTime.UtcNow;
}
