using MediatR;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, NotificationResult>
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _repository.GetById(request.Id);
        if (customer == null) return new NotificationResult("Cliente invalido", false);
        await _repository.DeleteAsync(customer);
        return new NotificationResult("Cliente excluido");
    }
}
