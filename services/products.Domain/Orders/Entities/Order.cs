using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Orders.Entities;

public class Order : Entity
{
    public Order(Customer customer, List<Item> itens)
    {
        Customer = customer;
        if (customer is null) throw new CustomException("Um pedido deve conter um cliente");
        Itens = itens;
        if (itens is null) throw new CustomException("Um pedido deve conter ao menos um item");
    }
    protected Order(){}

    public Customer Customer { get; private set; }
    public List<Item> Itens { get; private set; }
}
