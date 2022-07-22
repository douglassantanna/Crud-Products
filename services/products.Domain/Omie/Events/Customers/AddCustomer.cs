using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;

namespace products.Domain.Omie.Events.Customers;
public class AddCustomer : INotificationHandler<NewCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private const string OMIE_CALL = "IncluirCliente";
    private const string APP_KEY = "2648370684960";
    private const string APP_SECRET = "2310dba1bf1176707d8754e808b81f05";
    private readonly ILogger<AddCustomer> _logger;
    public AddCustomer(ILogger<AddCustomer> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task Handle(NewCustomer request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        ***********************Omie.Events.AddCustomer***********************
        **********Process to add a new customer at Omie initialized**********");

        try
        {
            var addresses = request.enderecoEntrega.Select(x =>
                new NewShippingAddress(
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
                        new NewCustomer
                            (
                                request.cnpj_cpf,
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
                **********Customer has been added to Omie.**********");

                var customer = _customerRepository.GetByCnpj_cpf(request.cnpj_cpf);
                customer.UpdateClienteOmieId(jsonResult.codigo_cliente_omie);
                await _customerRepository.UpdateAsync(customer);
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
public class NewCustomer : INotification
{
    public NewCustomer(
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
        List<NewShippingAddress> enderecoEntrega
        )
    {
        this.codigo_cliente_integracao = cnpj_cpf;
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
    public List<NewShippingAddress> enderecoEntrega { get; private set; }
}

public record OmieResult(
    double codigo_cliente_omie,
    string codigo_cliente_integracao,
    string codigo_status,
    string descricao_status
);
public record CustomerErrorResult(string faultstring, string faultcode);
public class CustomerRequest
{
    public CustomerRequest(string call, string app_key, string app_secrets, List<NewCustomer> param)
    {
        this.call = call;
        this.app_key = app_key;
        this.app_secret = app_secrets;
        this.param = param;
    }

    public string call { get; private set; }
    public string app_key { get; private set; }
    public string app_secret { get; private set; }
    public List<NewCustomer> param { get; private set; }

}

public record ErrorResponse(List<string> Headers);