using Flurl.Http;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Results;
using products.Domain.Customers.Interfaces;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;

namespace products.Domain.Infra.Omie;
public class OmieCustomerService : IOmieCustomer
{
    private const string OMIE_URL = "https://app.omie.com.br/api/v1/geral/clientes/";
    private readonly ILogger<OmieCustomerService> _logger;
    private readonly ICustomerRepository _customerRepository;

    public OmieCustomerService(ILogger<OmieCustomerService> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<OmieCreateCustomerResult> CreateCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
              .WithHeader("Content-type", "application/json")
              .WithHeader("accept", "application/json")
              .PostJsonAsync(request)
              .ReceiveJson<OmieCreateCustomerResult>();

        if (httpResult is null)
            throw new Exception("Erro ao criar cliente");

        var customer = _customerRepository.GetByCnpj_cpf(httpResult.codigo_cliente_integracao);
        customer.UpdateClienteOmieId(httpResult.codigo_cliente_omie);
        await _customerRepository.UpdateAsync(customer);

        _logger.LogInformation(@"
                **********Processo para criar cliente da Omie foi concluído.**********");
        return httpResult;
    }
    public async Task<OmieGetCustomerResult> GetCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
               .WithHeader("Content-type", "application/json")
               .WithHeader("accept", "application/json")
               .PostJsonAsync(request)
               .ReceiveJson<OmieGetCustomerResult>();

        _logger.LogInformation(@"
                **********Processo para obter cliente da Omie foi concluído.**********");
        if (httpResult is null)
            throw new Exception("Erro ao obter cliente");
        return httpResult;
    }
    public async Task<OmieCreateCustomerResult> UpdateCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
              .WithHeader("Content-type", "application/json")
              .WithHeader("accept", "application/json")
              .PostJsonAsync(request)
              .ReceiveJson<OmieCreateCustomerResult>();

        _logger.LogInformation(@"
                **********Processo para atualizar cliente da Omie foi concluído.**********");
        if (httpResult is null)
            throw new Exception("Erro ao atualizar cliente");
        return httpResult;
    }
    public async Task<OmieCreateCustomerResult> DeleteCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
             .WithHeader("Content-type", "application/json")
             .WithHeader("accept", "application/json")
             .PostJsonAsync(request)
             .ReceiveJson<OmieCreateCustomerResult>();

        _logger.LogInformation(@"
                **********Processo para excluir cliente da Omie foi concluído.**********");
        if (httpResult is null)
            throw new Exception("Erro ao excluir cliente");
        return httpResult;
    }
}