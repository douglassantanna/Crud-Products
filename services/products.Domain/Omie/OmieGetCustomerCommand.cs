using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Omie;
using products.Domain.Shared;

namespace product.Domain.Omie;
public record OmieGetCustomerCommand(double codigo_cliente_omie, string codigo_cliente_integracao) : IRequest<NotificationResult>;

public class OmieGetCustomerHandler : IRequestHandler<OmieGetCustomerCommand, NotificationResult>
{
    private const string OMIE_CALL = "ConsultarCliente";
    private const string APP_KEY = "2672934660396";
    private const string APP_SECRET = "b9fa7cb28d51ce793fa82ee32243efc8";
    private readonly IOmieCustomer _omieCustomer;
    private readonly ILogger<OmieGetCustomerHandler> _logger;

    public OmieGetCustomerHandler(IOmieCustomer omieCustomer, ILogger<OmieGetCustomerHandler> logger)
    {
        _omieCustomer = omieCustomer;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(OmieGetCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request is null");

        //create validation to OmieGetCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{OMIE_CALL}",
           app_key: $"{APP_KEY}",
           app_secrets: $"{APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.GetCustomer(body);

        return new NotificationResult("Great job!", true, result);
    }
}