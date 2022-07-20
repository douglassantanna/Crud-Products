using Flurl.Http;
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

    public async Task Handle(NewCustomer request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        ***********************Omie.Events.AddCustomer***********************
        **********Process to add a new customer at Omie initialized**********
        ");

        try
        {
            var addresses = request.EnderecosEntrega.Select(x =>
                new NewShippingAddress(
                    x.EntEndereco,
                    x.EntNumero,
                    x.EntComplemento,
                    x.EntBairro,
                    x.EntCEP,
                    x.EntEstado,
                    x.EntCidade)).ToList();

            if (request is not null)
            {
                var body = new CustomerRequest(
                    call: "IncluirCliente",
                    app_key: "2648370684960",
                    app_secrets: "2310dba1bf1176707d8754e808b81f05",
                    new()
                    {
                        new NewCustomer
                            (
                                request.Email,
                                request.Razao_social,
                                request.Nome_fantasia,
                                request.Cnpj_cpf,
                                request.Contato,
                                request.Telefone1_ddd,
                                request.Telefone1_numero,
                                request.Endereco,
                                request.Endereco_numero,
                                request.Bairro,
                                request.Complemento,
                                request.Estado,
                                request.Cidade,
                                request.Cep,
                                request.Contribuinte,
                                request.Observacao,
                                request.Pessoa_fisica,
                                addresses
                            )
                    }

                );
                _logger.LogInformation(body.ToString());
                var result = await "https://app.omie.com.br/api/v1/geral/clientes/"
                .WithHeader("Content-type", "application/json")
                .WithHeader("accept", "application/json")
                .SendJsonAsync(HttpMethod.Post, body);

                var jsonResponse = await result.GetJsonAsync<CustomerResponse>();
                _logger.LogInformation(@"
                **********Customer has been added to Omie.**********");
            }
        }
        catch (FlurlHttpException e)
        {
            // var error = await e.GetResponseJsonAsync<ErrorResponse>();
            // var errorToString = error.ToString();
            _logger.LogError(@"
            **********An error has returned from Omie: {0}**********
            ", e.Call.HttpResponseMessage);
        }
    }
}
public class NewCustomer : INotification
{
    public NewCustomer(
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
        List<NewShippingAddress> enderecosEntrega)
    {
        Email = email;
        Razao_social = razao_social;
        Nome_fantasia = nome_fantasia;
        Cnpj_cpf = cnpj_cpf;
        Contato = contato;
        Telefone1_ddd = telefone1_ddd;
        Telefone1_numero = telefone1_numero;
        Endereco = endereco;
        Endereco_numero = endereco_numero;
        Bairro = bairro;
        Complemento = complemento;
        Estado = estado;
        Cidade = cidade;
        Cep = cep;
        Contribuinte = contribuinte;
        Observacao = observacao;
        Pessoa_fisica = pessoa_fisica;
        EnderecosEntrega = enderecosEntrega;
    }

    public string Email { get; private set; }
    public string Razao_social { get; private set; }
    public string Nome_fantasia { get; private set; }
    public string Cnpj_cpf { get; private set; }
    public string Contato { get; private set; }
    public string Telefone1_ddd { get; private set; }
    public string Telefone1_numero { get; private set; }
    public string Endereco { get; private set; }
    public string Endereco_numero { get; private set; }
    public string Bairro { get; private set; }
    public string Complemento { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
    public string Cep { get; private set; }
    public string Contribuinte { get; private set; }
    public string Observacao { get; private set; }
    public string Pessoa_fisica { get; private set; }
    public List<NewShippingAddress> EnderecosEntrega { get; private set; }
}

public record CustomerResponse(
    int Codigo_cliente_omie,
    string Codigo_cliente_integracao,
    string Codigo_status,
    string Descricao_status
);
public class CustomerRequest
{
    public CustomerRequest(string call, string app_key, string app_secrets, List<NewCustomer> param)
    {
        Call = call;
        App_key = app_key;
        App_secrets = app_secrets;
        Param = param;
    }

    public string Call { get; private set; }
    public string App_key { get; private set; }
    public string App_secrets { get; private set; }
    public List<NewCustomer> Param { get; private set; }

}

public record ErrorResponse(List<string> Headers);