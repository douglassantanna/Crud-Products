namespace products.Domain.Shared;
public interface IProductNotification
{
    Task Send(object? data);
}