using System.ComponentModel.DataAnnotations;

namespace RESTAPIBankingApplication.Models.Requests;

public class TransactionRequest
{
    [Required]
    public Guid SenderAccId { get; set; }
    public Guid? ReceiverAccId { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}