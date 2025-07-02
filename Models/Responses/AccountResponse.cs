namespace RESTAPIBankingApplication.Models.Responses;

public class AccountResponse
{
    public Guid AccountId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}