using products.Domain.Shared;
public class Address : Entity
{
    public Address(string street, string zipCode, string city, string state)
    {
        Street = street;
        ZipCode = zipCode;
        City = city;
        State = state;
    }

    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }

}