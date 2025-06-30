namespace RESTAPIBankingApplication.Models;

public class Account
{
    public Guid AccountNumber { get; set; }
    public string? Name { get; set; } = null!;
    public decimal Balance { get; set; }
}