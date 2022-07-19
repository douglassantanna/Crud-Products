using MediatR;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, NotificationResult>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _repository.GetById(request.Id);
        if (customer == null) return new NotificationResult("Cliente invalido", false);
        customer.UpdateRazao_social(request.Razao_social);
        customer.UpdateEmail(request.Email);
        await _repository.UpdateAsync(customer);
        return new NotificationResult("Cliente atualizado");

    }
}
