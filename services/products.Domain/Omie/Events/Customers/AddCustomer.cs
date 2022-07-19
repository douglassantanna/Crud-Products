using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Customers.Commands;

namespace products.Domain.Omie.Events.Customers;
public class AddCustomer : INotificationHandler<NewCustomer>
{
    private readonly ILogger<AddCustomer> _logger;

    public AddCustomer(ILogger<AddCustomer> logger)
    {
        _logger = logger;
    }

    public Task Handle(NewCustomer request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        ***********************Omie.Events.AddCustomer***********************
        **********Process to add a new customer at Omie initialized**********
        ");
        
        throw new NotImplementedException();
    }
}
public record NewCustomer(
    string Email,
    string Razao_social,
    string Nome_fantasia,
    string Cnpj_cpf,
    string Contato,
    string Telefone1_ddd,
    string Telefone1_numero,
    string Endereco,
    string Endereco_numero,
    string Bairro,
    string Complemento,
    string Estado,
    string Cidade,
    string Cep,
    string Contribuinte,
    string Observacao,
    string Pessoa_fisica,
    NewShipingAddress EnderecoEntrega
) : INotification;