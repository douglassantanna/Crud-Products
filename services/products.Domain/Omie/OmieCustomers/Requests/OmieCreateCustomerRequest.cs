using MediatR;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieCreateCustomerRequest : IRequest<NotificationResult>
{
    public OmieCreateCustomerRequest(
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
        string pessoa_fisica)
    {
        this.codigo_cliente_integracao = codigo_cliente_integracao;
        this.email = email;
        this.razao_social = razao_social;
        this.cnpj_cpf = cnpj_cpf;
        this.contato = contato;
        this.telefone1_numero = telefone1_numero;
        this.endereco = endereco;
        this.endereco_numero = endereco_numero;
        this.bairro = bairro;
        this.complemento = complemento;
        this.estado = estado.ToUpper();
        this.cidade = cidade;
        this.cep = cep;
        this.contribuinte = contribuinte.ToUpper();
        this.observacao = observacao;
        this.pessoa_fisica = pessoa_fisica.ToUpper();
    }

    public string codigo_cliente_integracao { get; init; }
    public string email { get; init; }
    public string razao_social { get; init; }
    public string cnpj_cpf { get; init; }
    public string contato { get; init; }
    public string telefone1_numero { get; init; }
    public string endereco { get; init; }
    public string endereco_numero { get; init; }
    public string bairro { get; init; }
    public string complemento { get; init; }
    public string estado { get; init; }
    public string cidade { get; init; }
    public string cep { get; init; }
    public string contribuinte { get; init; }
    public string observacao { get; init; }
    public string pessoa_fisica { get; init; }
}


public class OmieCreateCustomerHandler : IRequestHandler<OmieCreateCustomerRequest, NotificationResult>
{
    private readonly IOmieCustomer _omieCustomer;
    private readonly OmieConfigurations _configurations;

    public OmieCreateCustomerHandler(IOmieCustomer omieCustomer, OmieConfigurations configurations)
    {

        _omieCustomer = omieCustomer;
        _configurations = configurations;
        _configurations.OMIE_CALL = "IncluirCliente";
    }

    public async Task<NotificationResult> Handle(OmieCreateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request nao pode ser nulo", false);

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