using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using products.Domain.Omie;

namespace products.Domain.Infra.Omie;
public class OmieCustomerService : IOmieCustomer
{
    private const string OMIE_URL = "https://app.omie.com.br/api/v1/geral/clientes/";
    private const string OMIE_CALL = "ConsultarCliente";
    private const string APP_KEY = "2672934660396";
    private const string APP_SECRET = "b9fa7cb28d51ce793fa82ee32243efc8";
    private readonly ILogger<OmieCustomerService> _logger;

    public OmieCustomerService(ILogger<OmieCustomerService> logger)
    {
        _logger = logger;
    }

    public async Task<OmieCustomerResult> GetCustomer(OmieGeneralRequest request)
    {
        var httpResult = await $"{OMIE_URL}"
               .WithHeader("Content-type", "application/json")
               .WithHeader("accept", "application/json")
               .PostJsonAsync(request);

        var dataResult = await httpResult.GetStringAsync();
        var response = JsonSerializer.Deserialize<OmieCustomerResult>(dataResult);
        _logger.LogInformation(@"
                **********Process to get customer from Omie has been completed.**********");
        if (response is null)
            throw new Exception("Erro ao criar cliente");
        return response;
    }
}