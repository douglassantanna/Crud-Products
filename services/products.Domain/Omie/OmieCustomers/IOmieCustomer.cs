using product.Domain.Omie.OmieCustomers.Results;

namespace products.Domain.Omie.OmieCustomers;
public interface IOmieCustomer
{
    Task<OmieGetCustomerResult> GetCustomer(OmieGeneralRequest request);
    Task<OmieCreateCustomerResult> CreateCustomer(OmieGeneralRequest request);
    Task<OmieCreateCustomerResult> DeleteCustomer(OmieGeneralRequest request);
    Task<OmieCreateCustomerResult> UpdateCustomer(OmieGeneralRequest request);
}