using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Results;
using products.Domain.Customers.Contracts;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace products.Domain.Infra.Omie;
public class OmieCustomerService : IOmieCustomer
{
    private readonly ILogger<OmieCustomerService> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly OmieConfigurations _configurations;

    public OmieCustomerService(ILogger<OmieCustomerService> logger, ICustomerRepository customerRepository, OmieConfigurations configurations)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _configurations = configurations;
    }

    public async Task<NotificationResult> CreateCustomer(OmieGeneralRequest request)
    {
        try
        {
            var httpResult = await _configurations.OMIE_URL
                  .AppendPathSegment("clientes/")
                  .WithHeader("Content-type", "application/json")
                  .WithHeader("accept", "application/json")
                  .PostJsonAsync(request)
                  .ReceiveJson<OmieCreateCustomerResult>();

            var customer = _customerRepository.GetByCnpj_cpf(httpResult.codigo_cliente_integracao);
            customer.UpdateClienteOmieId(httpResult.codigo_cliente_omie);
            await _customerRepository.UpdateAsync(customer);
            return new("", true, new { httpResult.descricao_status });
        }
        catch (FlurlHttpException ex)
        {
            var errors = await ex.GetResponseJsonAsync<ErrorResult>();
            return new("An error occured:", false, new { errors });
        }
    }
    public async Task<NotificationResult> GetCustomer(OmieGeneralRequest request)
    {
        try
        {
            var httpResult = await _configurations.OMIE_URL
               .AppendPathSegment("clientes/")
               .WithHeader("Content-type", "application/json")
               .WithHeader("accept", "application/json")
               .PostJsonAsync(request)
               .ReceiveJson<OmieGetCustomerResult>();

            return new("", true, new { httpResult });
        }
        catch (FlurlHttpException ex)
        {
            var errors = await ex.GetResponseJsonAsync<ErrorResult>();
            return new("An error occured:", false, new { errors });
        }

    }
    public async Task<NotificationResult> UpdateCustomer(OmieGeneralRequest request)
    {
        try
        {
            var httpResult = await _configurations.OMIE_URL
                .AppendPathSegment("clientes/")
                .WithHeader("Content-type", "application/json")
                .WithHeader("accept", "application/json")
                .PostJsonAsync(request)
                .ReceiveJson<OmieCreateCustomerResult>();

            return new("", true, new { httpResult });
        }
        catch (FlurlHttpException ex)
        {
            var errors = await ex.GetResponseJsonAsync<ErrorResult>();
            return new("An error occured:", false, new { errors });
        }
    }
    public async Task<NotificationResult> DeleteCustomer(OmieGeneralRequest request)
    {
        try
        {
            var httpResult = await _configurations.OMIE_URL
                .AppendPathSegment("clientes/")
                .WithHeader("Content-type", "application/json")
                .WithHeader("accept", "application/json")
                .PostJsonAsync(request)
                .ReceiveJson<OmieCreateCustomerResult>();

            return new("", true, new { httpResult });
        }
        catch (FlurlHttpException ex)
        {
            var errors = await ex.GetResponseJsonAsync<ErrorResult>();
            return new("An error occured:", false, new { errors });
        }
    }
}