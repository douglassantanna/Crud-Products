using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Orders;

public class Order : Entity
{
    public Order(Customer customer, List<Item> itens)
    {
        Customer = customer;
        Itens = itens;
    }

    public Customer Customer { get; private set; }
    public List<Item> Itens { get; private set; }
}
