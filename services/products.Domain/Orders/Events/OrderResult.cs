using System.Runtime.Intrinsics.X86;
using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Shared;

namespace products.Domain.Orders.Events;
public class OrderResult : INotification
{
    public NotificationResult? Result {get;set;}
}
public class OrderResultHandler : INotificationHandler<OrderResult>
{
    private readonly ILogger<OrderResult> _logger;
    // private readonly IProductNotification _productNotification;

    public OrderResultHandler(ILogger<OrderResult> logger)
    {
        _logger = logger;
        // _productNotification = productNotification;
    }

    public Task Handle(OrderResult notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notification of a new order created received: {0}", notification);
        return Task.CompletedTask;
        // await _productNotification.Send(notification.Result);
    }
}