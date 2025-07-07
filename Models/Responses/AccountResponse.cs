namespace RESTAPIBankingApplication.Models.Responses;

public record AccountResponse(Guid AccountId, string Name, decimal Balance);