using BC = BCrypt.Net.BCrypt;

namespace products.Domain.UnitTests.ApplicationCore.Entities;
public class HasPassword
{
    [Fact]
    public void check_hashing_works_for_valid_password()
    {
        string password = "myDummyPassword!";
        string hashedPassword = BC.HashPassword(password);

        var passwordsMatch = BC.Verify(password, hashedPassword);
        Assert.True(passwordsMatch);
    }
}