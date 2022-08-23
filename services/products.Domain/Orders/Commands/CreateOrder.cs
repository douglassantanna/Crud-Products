using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Customers.Contracts;
using products.Domain.Itens.Entities;
using products.Domain.Orders.Events;
using products.Domain.Orders.Contracts;
using products.Domain.Shared;

namespace products.Domain.Orders.Commands;

public record CreateOrderCommand(int CustomerID, List<Item> Itens) : IRequest<NotificationResult>;
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(c => c.CustomerID).NotNull().WithMessage("Um pedido deve conter um cliente");
        RuleForEach(c => c.Itens).NotNull().WithMessage("Um pedido deve conter ao menos um item");
    }
}
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, NotificationResult>
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateOrderHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(ICustomerRepository customerRepository, ILogger<CreateOrderHandler> logger, IMediator mediator, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<NotificationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Process to create an order initializing");
        var validation = new CreateOrderValidator();
        var validated = validation.Validate(request);
        if (!validated.IsValid)
        {
            var message = "Process to create the order has failed due to validation errors";
            _logger.LogInformation($"{message}, Errors: {string.Join(",", validated.Errors.Select(e => e.ErrorMessage))}");
            var errors = new NotificationResult(message, false, validated.Errors);
            await _mediator.Publish(new OrderResult() { Result = errors });
            return errors;
        }

        _logger.LogInformation("Creating order");
        var customer = _customerRepository.GetById(request.CustomerID);
        var itens = request.Itens.Select(x => new Item(x.Name, x.Price)).ToList();
        // var order = new Order(customer, itens);
        // await _orderRepository.CreateAsync(order);
        await _mediator.Publish(new OrderResult() { Result = new("Pedido criado") });
        _logger.LogInformation("Order created");
        return new NotificationResult("Pedido criado");

    }
}
