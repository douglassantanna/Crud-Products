namespace products.Domain.Entities;

public class Item : Entity
{
    public Item(string name, double price)
    {
        Name = name;
        Price = price;
        CreatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; }
    public double Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedWhen { get; private set; }
    public void ChangeName(string name) => Name = name;
    public void ChangePrice(double price) => Price = price;
    public void UpdatedAt() => UpdatedWhen = DateTime.UtcNow;
}
