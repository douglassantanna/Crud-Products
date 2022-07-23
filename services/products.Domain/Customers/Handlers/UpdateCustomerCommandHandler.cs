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
        var validation = new UpdateCustomerValidator();
        var validated = validation.Validate(request);

        var customer = _repository.GetById(request.Id);
        if (customer == null) return new NotificationResult("Cliente invalido", false);
        customer.UpdateCustomer(
            customer.Email,
            customer.Razao_social,
            customer.Nome_fantasia,
            customer.Cnpj_cpf,
            customer.Contato,
            customer.Telefone1_ddd,
            customer.Telefone1_numero,
            customer.Endereco,
            customer.Endereco_numero,
            customer.Bairro,
            customer.Complemento,
            customer.Estado,
            customer.Cidade,
            customer.Cep,
            customer.Contribuinte,
            customer.Observacao,
            customer.Pessoa_fisica,
            customer.EnderecoEntrega
            );
        await _repository.UpdateAsync(customer);
        return new NotificationResult("Cliente atualizado");

    }
}
