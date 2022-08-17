using MediatR;
using products.Domain.Omie;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace product.Domain.Omie.OmieCustomers.Requests;
public record OmieDeleteCustomerRequest(double codigo_cliente_omie, string? codigo_cliente_integracao) : IRequest<NotificationResult>;

public class OmieDeleteCustomerHandle : IRequestHandler<OmieDeleteCustomerRequest, NotificationResult>
{
    private readonly IOmieCustomer _omieCustomer;
    private readonly OmieConfigurations _configurations;

    public OmieDeleteCustomerHandle(IOmieCustomer omieCustomer, OmieConfigurations configurations)
    {
        _omieCustomer = omieCustomer;
        _configurations = configurations;
        _configurations.OMIE_CALL = "ExcluirCliente";

    }

    public async Task<NotificationResult> Handle(OmieDeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request is null) return new NotificationResult("Request is null");

        //create validation to OmieDeleteCustomerCommand
        var body = new OmieGeneralRequest(
           call: $"{_configurations.OMIE_CALL}",
           app_key: $"{_configurations.APP_KEY}",
           app_secrets: $"{_configurations.APP_SECRET}",
           new() { request });

        var result = await _omieCustomer.DeleteCustomer(body);
        if (!result.Success) return new("An error occured:", false, new { result.Data });

        return new NotificationResult("", true, result);
    }
}