using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieUpdateCustomerRequest(
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
        string pessoa_fisica,
        List<AddressToUpdateOmie> enderecoEntrega
) : IRequest<NotificationResult>;
public record AddressToUpdateOmie(
            string EntEndereco,
            string EntNumero,
            string EntComplemento,
            string EntBairro,
            string EntCEP,
            string EntEstado,
            string EntCidade);
public class OmieUpdateCustomerHandler : IRequestHandler<OmieUpdateCustomerRequest, NotificationResult>
{
    private const string OMIE_CALL = "UpsertCliente";
    private const string APP_KEY = "2699300300697";
    private const string APP_SECRET = "b7ab98a7fc57e3aba0639bcbf393ff39";
    private readonly IOmieCustomer _omieCustomer;
    private readonly ILogger<OmieUpdateCustomerHandler> _logger;

    public OmieUpdateCustomerHandler(IOmieCustomer omieCustomer, ILogger<OmieUpdateCustomerHandler> logger)
    {
        _omieCustomer = omieCustomer;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(OmieUpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request is null");

        //create validation to OmieCreateCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{OMIE_CALL}",
           app_key: $"{APP_KEY}",
           app_secrets: $"{APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.UpdateCustomer(body);

        return new NotificationResult("Great job! Customer updated.", true, result);
    }
}