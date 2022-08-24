using products.Domain.Shared;

namespace products.Domain.Auth.Entities;
public class User : Entity
{
    public User(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
    private User() { }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}