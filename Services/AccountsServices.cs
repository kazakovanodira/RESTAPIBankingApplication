using RESTAPIBankingApplication.Data;
using RESTAPIBankingApplication.Interface;
using RESTAPIBankingApplication.Models;
using RESTAPIBankingApplication.Models.Requests;
using RESTAPIBankingApplication.Models.Responses;

namespace RESTAPIBankingApplication.Services;

public class AccountsServices : IAccountsService
{
    private AccountsContext _context;
    public AccountsServices(AccountsContext context)
    {
        _context = context;
    }


    public ApiResponse<AccountResponse> CreateAccount(CreateAccountRequest request)
    {
        var account = new  Account
        {
            Name = request.Name,
            Balance = 0,
            AccountNumber = Guid.NewGuid(),
        };
        
        _context.Accounts.Add(account);
        var response = new ApiResponse<AccountResponse>
        {
            Result = new AccountResponse
            {
                AccountId = account.AccountNumber,
                Balance = account.Balance,
                Name = account.Name,
            },
        };
        
        _context.SaveChanges();
        return response;
    }

    public ApiResponse<AccountResponse> GetAccount(AccountRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.AccountId);
        var response = new ApiResponse<AccountResponse>
        {
            Result = new AccountResponse
            {
                AccountId = account.AccountNumber,
                Balance = account.Balance,
                Name = account.Name,
            }
        };
        
        return response;
    }

    public ApiResponse<BalanceResponse> MakeDeposit(TransactionRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        account.Balance += request.Amount;
        _context.SaveChanges();
        
        var response = new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  account.Balance,
            }
        };
        
        return response;
    }

    public ApiResponse<BalanceResponse> MakeWithdraw(TransactionRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        account.Balance -= request.Amount;
        _context.SaveChanges();
        
        var response = new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  account.Balance,
            }
        };
        
        return response;
    }

    public ApiResponse<BalanceResponse> MakeTransfer(TransactionRequest request)
    {
        var sender = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        var receiver = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.ReceiverAccId);
        sender.Balance -= request.Amount;
        receiver.Balance += request.Amount;
        _context.SaveChanges();
        
        var response = new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  sender.Balance,
            }
        };
        
        return response;
    }

    public ApiResponse<BalanceResponse> CheckBalance(AccountRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.AccountId);
        
        var response = new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance = account.Balance,
            }
        };
        
        return response;
    }
}