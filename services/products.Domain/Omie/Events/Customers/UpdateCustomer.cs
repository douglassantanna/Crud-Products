using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;

namespace products.Domain.Omie.Events.Customers;
public class UpdateCustomer : INotificationHandler<CustomerToUpdate>
{
    private readonly ICustomerRepository _customerRepository;
    private const string OMIE_CALL = "UpsertCliente";
    private const string APP_KEY = "2648370684960";
    private const string APP_SECRET = "2310dba1bf1176707d8754e808b81f05";
    private readonly ILogger<UpdateCustomer> _logger;
    public UpdateCustomer(ILogger<UpdateCustomer> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerToUpdate request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        ***********************Omie.Events.UpdateCustomer***********************
        **********Process to add a new customer at Omie initialized**********");

        try
        {
            var addresses = request.enderecoEntrega.Select(x =>
                new AddressToUpdateOmie(
                    x.entEndereco,
                    x.entNumero,
                    x.entComplemento,
                    x.entBairro,
                    x.entCEP,
                    x.entEstado,
                    x.entCidade)).ToList();

            if (request is not null)
            {
                var body = new CustomerRequest(
                    call: $"{OMIE_CALL}",
                    app_key: $"{APP_KEY}",
                    app_secrets: $"{APP_SECRET}",
                    new(){
                        new CustomerToUpdate
                            (
                                request.codigo_cliente_integracao,
                                request.email,
                                request.razao_social,
                                request.nome_fantasia,
                                request.cnpj_cpf,
                                request.contato,
                                request.telefone1_ddd,
                                request.telefone1_numero,
                                request.endereco,
                                request.endereco_numero,
                                request.bairro,
                                request.complemento,
                                request.estado,
                                request.cidade,
                                request.cep,
                                request.contribuinte,
                                request.observacao,
                                request.pessoa_fisica,
                                addresses
                            )}
                );

                var result = await "https://app.omie.com.br/api/v1/geral/clientes/"
                .WithHeader("Content-type", "application/json")
                .WithHeader("accept", "application/json")
                .PostJsonAsync(body);

                var dataResult = await result.GetStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<OmieResult>(dataResult);
                _logger.LogInformation(@"
                **********Customer has been updated to Omie.**********");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(@"
            **********An error has returned from Omie: {0}**********", e.Message);
        }
        finally
        {
            _logger.LogInformation(@"
            **********Process has ended.**********");
        }
    }
}
public record AddressToUpdateOmie(
        string entEndereco,
        string entNumero,
        string entComplemento,
        string entBairro,
        string entCEP,
        string entEstado,
        string entCidade
);
public class CustomerToUpdate : INotification
{
    public CustomerToUpdate(
        string codigo_cliente_integracao,
        string email,
        string razao_social,
        string nome_fantasia,
        string cnpj_cpf,
        string contato,
        string telefone1_ddd,
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
        )
    {
        this.codigo_cliente_integracao = codigo_cliente_integracao;
        this.email = email;
        this.razao_social = razao_social;
        this.nome_fantasia = nome_fantasia;
        this.cnpj_cpf = cnpj_cpf;
        this.contato = contato;
        this.telefone1_ddd = telefone1_ddd;
        this.telefone1_numero = telefone1_numero;
        this.endereco = endereco;
        this.endereco_numero = endereco_numero;
        this.bairro = bairro;
        this.complemento = complemento;
        this.estado = estado;
        this.cidade = cidade;
        this.cep = cep;
        this.contribuinte = contribuinte;
        this.observacao = observacao;
        this.pessoa_fisica = pessoa_fisica;
        this.enderecoEntrega = enderecoEntrega;
    }
    public string codigo_cliente_integracao { get; private set; }
    public string email { get; private set; }
    public string razao_social { get; private set; }
    public string nome_fantasia { get; private set; }
    public string cnpj_cpf { get; private set; }
    public string contato { get; private set; }
    public string telefone1_ddd { get; private set; }
    public string telefone1_numero { get; private set; }
    public string endereco { get; private set; }
    public string endereco_numero { get; private set; }
    public string bairro { get; private set; }
    public string complemento { get; private set; }
    public string estado { get; private set; }
    public string cidade { get; private set; }
    public string cep { get; private set; }
    public string contribuinte { get; private set; }
    public string observacao { get; private set; }
    public string pessoa_fisica { get; private set; }
    public List<AddressToUpdateOmie> enderecoEntrega { get; private set; }
}