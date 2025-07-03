using RESTAPIBankingApplication.Models.Requests;
using RESTAPIBankingApplication.Models.Responses;

namespace RESTAPIBankingApplication.Interface;

public interface IAccountsService
{
    ApiResponse<AccountResponse> CreateAccount(CreateAccountRequest request);
    ApiResponse<AccountResponse> GetAccount(AccountRequest request);
    ApiResponse<BalanceResponse> MakeDeposit(TransactionRequest request);
    ApiResponse<BalanceResponse> MakeWithdraw(TransactionRequest request);
    ApiResponse<BalanceResponse> MakeTransfer(TransactionRequest request);
}