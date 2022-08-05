using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieDeleteCustomerRequest(double codigo_cliente_omie, string codigo_cliente_integracao) : IRequest<NotificationResult>;

public class OmieDeleteCustomerHandle : IRequestHandler<OmieDeleteCustomerRequest, NotificationResult>
{
    private const string OMIE_CALL = "ExcluirCliente";
    private const string APP_KEY = "2699300300697";
    private const string APP_SECRET = "b7ab98a7fc57e3aba0639bcbf393ff39";
    private readonly IOmieCustomer _omieCustomer;
    private readonly ILogger<OmieDeleteCustomerHandle> _logger;

    public OmieDeleteCustomerHandle(IOmieCustomer omieCustomer, ILogger<OmieDeleteCustomerHandle> logger)
    {
        _omieCustomer = omieCustomer;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(OmieDeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request is null");

        //create validation to OmieGetCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{OMIE_CALL}",
           app_key: $"{APP_KEY}",
           app_secrets: $"{APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.DeleteCustomer(body);

        return new NotificationResult("Great job!", true, result);
    }
}