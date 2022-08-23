using product.Domain.Omie.OmieCustomers.Results;
using products.Domain.Shared;

namespace products.Domain.Omie.OmieCustomers;
public interface IOmieCustomer
{
    Task<NotificationResult> GetCustomer(OmieGeneralRequest request);
    Task<NotificationResult> CreateCustomer(OmieGeneralRequest request);
    Task<NotificationResult> DeleteCustomer(OmieGeneralRequest request);
    Task<NotificationResult> UpdateCustomer(OmieGeneralRequest request);
}