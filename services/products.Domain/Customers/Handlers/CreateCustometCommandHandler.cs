using MediatR;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class CreateCustometCommandHandler : IRequestHandler<CreateCustomerCommand, NotificationResult>
{
    private readonly ICustomerRepository _repository;

    public CreateCustometCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.FullName, request.Email, request.BirthDate);
        if (string.IsNullOrEmpty(customer.FullName)) return new NotificationResult("Nome não pode ser vázio.", false);
        await _repository.CreateAsync(customer);
        return new NotificationResult("Cliente criado");
    }
}
