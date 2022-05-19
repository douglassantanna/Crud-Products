using System;

namespace api.Product.Models
{
    public class Item
    {
        public Item(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public void ChangeName(string name) => Name = name;
        public void ChangePrice(double price) => Price = price;
    }
}
