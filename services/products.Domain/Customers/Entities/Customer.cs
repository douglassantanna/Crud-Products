using products.Domain.Shared;

namespace products.Domain.Customers.Entities;

public class Customer : Entity
{
    public Customer(string? fullName, string? email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public string? FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Email { get; private set; }
    public void UpdateFullName(string fullName) => FullName = fullName;
    public void UpdateEmail(string email) => Email = email;
    public void UpdateBirthDate(DateTime birthDate) => BirthDate = birthDate;
}

