namespace RESTAPIBankingApplication.Models.Requests;

public class TransactionRequest
{
    public Guid? SenderAccId { get; set; }
    public Guid? ReceiverAccId { get; set; }
    public decimal Amount { get; set; }
}