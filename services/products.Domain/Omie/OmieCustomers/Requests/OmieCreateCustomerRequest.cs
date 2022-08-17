using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieCreateCustomerRequest(
     string codigo_cliente_integracao,
        string email,
        string razao_social,
        string cnpj_cpf,
        string contato,
        string telefone1_numero,
        string endereco,
        string endereco_numero,
        string bairro,
        string complemento,
        string estado,
        string cidade,
        string cep,
        string contribuinte,
        string observacao,
        string pessoa_fisica
) : IRequest<NotificationResult>;

public class OmieCreateCustomerHandler : IRequestHandler<OmieCreateCustomerRequest, NotificationResult>
{
    private readonly IOmieCustomer _omieCustomer;
    private readonly ILogger<OmieCreateCustomerHandler> _logger;
    private readonly OmieConfigurations _configurations;

    public OmieCreateCustomerHandler(IOmieCustomer omieCustomer, ILogger<OmieCreateCustomerHandler> logger, OmieConfigurations configurations)
    {

        _omieCustomer = omieCustomer;
        _logger = logger;
        _configurations = configurations;
        _configurations.OMIE_CALL = "IncluirCliente";
    }

    public async Task<NotificationResult> Handle(OmieCreateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request nao pode ser nulo");

        //create validation to OmieCreateCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{_configurations.OMIE_CALL}",
           app_key: $"{_configurations.APP_KEY}",
           app_secrets: $"{_configurations.APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.CreateCustomer(body);
        if (!result.Success) return new("An error occured:", false, new { result.Data });

        return new("", true, result);
    }
}