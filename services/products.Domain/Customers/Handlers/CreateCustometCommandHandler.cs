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
        if (string.IsNullOrEmpty(customer.FullName))
            return new NotificationResult("Nome não pode ser vázio.", false);

        var emailExists = _repository.EmailExists(request.Email);
        if (emailExists)
            return new NotificationResult("Um cliente com esse email ja existe.", false);

        var age = _repository.UnderAge(request.BirthDate);
        if (age < 18)
            return new NotificationResult("Idade deve ser maior ou igual a 18 anos.", false);

        await _repository.CreateAsync(customer);
        return new NotificationResult("Cliente criado");
    }
}
