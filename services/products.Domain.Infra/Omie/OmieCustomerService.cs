using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Results;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;

namespace products.Domain.Infra.Omie;
public class OmieCustomerService : IOmieCustomer
{
    private const string OMIE_URL = "https://app.omie.com.br/api/v1/geral/clientes/";
    private readonly ILogger<OmieCustomerService> _logger;

    public OmieCustomerService(ILogger<OmieCustomerService> logger)
    {
        _logger = logger;
    }

    public async Task<OmieCreateCustomerResult> CreateCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
              .WithHeader("Content-type", "application/json")
              .WithHeader("accept", "application/json")
              .PostJsonAsync(request);

        var dataResult = await httpResult.GetStringAsync();
        var response = JsonSerializer.Deserialize<OmieCreateCustomerResult>(dataResult);
        _logger.LogInformation(@"
                **********Processo para criar cliente da Omie foi concluído.**********");
        if (response is null)
            throw new Exception("Erro ao criar cliente");
        return response;
    }
    public async Task<OmieGetCustomerResult> GetCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
               .WithHeader("Content-type", "application/json")
               .WithHeader("accept", "application/json")
               .PostJsonAsync(request);

        var dataResult = await httpResult.GetStringAsync();
        var response = JsonSerializer.Deserialize<OmieGetCustomerResult>(dataResult);
        _logger.LogInformation(@"
                **********Processo para obter cliente da Omie foi concluído.**********");
        if (response is null)
            throw new Exception("Erro ao obter cliente");
        return response;
    }
    public async Task<OmieCreateCustomerResult> UpdateCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
              .WithHeader("Content-type", "application/json")
              .WithHeader("accept", "application/json")
              .PostJsonAsync(request);

        var dataResult = await httpResult.GetStringAsync();
        var response = JsonSerializer.Deserialize<OmieCreateCustomerResult>(dataResult);
        _logger.LogInformation(@"
                **********Processo para atualizar cliente da Omie foi concluído.**********");
        if (response is null)
            throw new Exception("Erro ao atualizar cliente");
        return response;
    }
    public async Task<OmieCreateCustomerResult> DeleteCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
             .WithHeader("Content-type", "application/json")
             .WithHeader("accept", "application/json")
             .PostJsonAsync(request);

        var dataResult = await httpResult.GetStringAsync();
        var response = JsonSerializer.Deserialize<OmieCreateCustomerResult>(dataResult);
        _logger.LogInformation(@"
                **********Processo para excluir cliente da Omie foi concluído.**********");
        if (response is null)
            throw new Exception("Erro ao excluir cliente");
        return response;
    }
}