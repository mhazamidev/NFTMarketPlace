namespace NFTMarketPlace.WebApp.Models;

public record class Login
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public record class LoginFromCompass
{
    public string Username { get; set; }
    public Guid EmployeeId { get; set; }
}
