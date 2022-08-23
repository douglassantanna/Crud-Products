using MediatR;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieGetCustomerRequest(double codigo_cliente_omie, string codigo_cliente_integracao) : IRequest<NotificationResult>;

public class OmieGetCustomerHandler : IRequestHandler<OmieGetCustomerRequest, NotificationResult>
{
    private readonly IOmieCustomer _omieCustomer;
    private readonly OmieConfigurations _configurations;

    public OmieGetCustomerHandler(IOmieCustomer omieCustomer, OmieConfigurations configurations)
    {
        _omieCustomer = omieCustomer;
        _configurations = configurations;
        _configurations.OMIE_CALL = "ConsultarCliente";
    }

    public async Task<NotificationResult> Handle(OmieGetCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request nao pode ser nulo.");

        //create validation to OmieGetCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{_configurations.OMIE_CALL}",
           app_key: $"{_configurations.APP_KEY}",
           app_secrets: $"{_configurations.APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.GetCustomer(body);
        if (!result.Success) return new("An error occured:", false, new { result.Data });

        return new NotificationResult("", true, result);
    }
}