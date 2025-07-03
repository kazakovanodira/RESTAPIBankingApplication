using System.ComponentModel.DataAnnotations;

namespace RESTAPIBankingApplication.Models.Requests;

public class AccountRequest
{
    public Guid AccountId { get; set; }
}