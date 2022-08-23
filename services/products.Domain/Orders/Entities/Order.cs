using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Orders.Entities;

public class Order : Entity
{
    public Order(Customer customer, List<Item> itens, Address shipToAddress, PaymentMethod paymenthMethod, decimal total)
    {
        Customer = customer;
        Itens = itens;
        ShipToAddress = shipToAddress;
        PaymenthMethod = paymenthMethod;
        Total = total;
    }
    protected Order() { }

    public Customer Customer { get; private set; }
    public List<Item> Itens { get; private set; }
    public Address ShipToAddress { get; set; }
    public PaymentMethod PaymenthMethod { get; set; }
    public decimal Total { get; set; }
    public string Receipt { get; set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;

}
