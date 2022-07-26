using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Customers.Interfaces;
using products.Domain.Omie.Shared;

namespace products.Domain.Omie.Events.Customers;

public class DeleteCustomer : INotificationHandler<CustomerToDelete>
{
    private readonly ICustomerRepository _customerRepository;
    private const string OMIE_URL = "https://app.omie.com.br/api/v1/geral/clientes/";
    private const string OMIE_CALL = "ExcluirCliente";
    private const string APP_KEY = "2648370684960";
    private const string APP_SECRET = "2310dba1bf1176707d8754e808b81f05";
    private readonly ILogger<DeleteCustomer> _logger;

    public DeleteCustomer(ILogger<DeleteCustomer> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerToDelete request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        ***********************Omie.Events.DeleteCustomer***********************
        **********Process to delete customer at Omie initialized**********");
        try
        {
            if (request is not null)
            {
                var body = new OmieRequest(
                    call: $"{OMIE_CALL}",
                    app_key: $"{APP_KEY}",
                    app_secrets: $"{APP_SECRET}",
                    new(){
                        new CustomerToDelete
                            (
                                request.codigo_cliente_omie,
                                request.codigo_cliente_integracao
                            )}
                );

                var result = await $"{OMIE_URL}"
                .WithHeader("Content-type", "application/json")
                .WithHeader("accept", "application/json")
                .PostJsonAsync(body);
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
public class CustomerToDelete : INotification
{
    public CustomerToDelete(double codigo_cliente_omie, string codigo_cliente_integracao)
    {
        this.codigo_cliente_omie = codigo_cliente_omie;
        this.codigo_cliente_integracao = codigo_cliente_integracao;
    }

    public double codigo_cliente_omie { get; private set; }
    public string codigo_cliente_integracao { get; private set; }
}
