using MediatR;
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
    private readonly OmieConfigurations _configurations;
    private readonly IOmieCustomer _omieCustomer;

    public OmieUpdateCustomerHandler(IOmieCustomer omieCustomer, OmieConfigurations configurations)
    {
        _omieCustomer = omieCustomer;
        _configurations = configurations;
        _configurations.OMIE_CALL = "UpsertCliente";

    }

    public async Task<NotificationResult> Handle(OmieUpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request cant be null", false);

        //create validation to OmieUpdateCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{_configurations.OMIE_CALL}",
           app_key: $"{_configurations.APP_KEY}",
           app_secrets: $"{_configurations.APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.UpdateCustomer(body);
        if (!result.Success) return new("An error occured:", false, new { result.Data });

        return new NotificationResult("", true, new { result });
    }
}