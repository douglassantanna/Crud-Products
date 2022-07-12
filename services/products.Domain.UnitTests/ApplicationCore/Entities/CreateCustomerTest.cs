using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Exceptions;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateCustomerTest
{


    [Fact]
    public void Create_customer_with_success()
    {
        var newCustomer = new Customer(fullName: "Douglas", email: "douglas@teste.com", birthDate: DateTime.Now.AddYears(-19));
        Assert.NotNull(newCustomer);
    }

    [Theory]
    [InlineData("Douglas", "")]
    public void WhenCreatingCustomer_WithNoFullNameOrNoEmail_ThrowExcepiton(string fullName, string email)
    {   
        var exception = Assert.Throws<Exception>(() => new Customer(fullName, email, DateTime.Now.AddYears(-19)));
    }
}