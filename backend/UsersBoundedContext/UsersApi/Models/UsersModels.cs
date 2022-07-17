namespace UsersApi.Models;

public class NewUserCreateRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Cc { get; set; } = String.Empty;
}



public class User
{
    public string Id { get; set; } = String.Empty;
    public long CreatedTimestamp { get; set; }
    public string Username { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}
