using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Shared;

namespace products.Domain.Customers.Events;
public class CustomerResult : INotification
{
    public NotificationResult? Result { get; set; }
}
public class CustomerResultHandler : INotificationHandler<CustomerResult>
{
    private readonly ILogger<CustomerResult> _logger;
    // private readonly IProductNotification _productNotification;

    public CustomerResultHandler(ILogger<CustomerResult> logger)
    {
        _logger = logger;
        // _productNotification = productNotification;
    }

    public Task Handle(CustomerResult notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("**********Notification of a new customer created received: {0}**********", notification);
        return Task.CompletedTask;
        // await _productNotification.Send(notification.Result);
    }
}