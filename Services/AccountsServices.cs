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
            HttpStatusCode = 200
        };
        
        _context.SaveChanges();
        return response;
    }

    public ApiResponse<AccountResponse> GetAccount(AccountRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.AccountId);
        if (account is null)
        {
            return new ApiResponse<AccountResponse>
            {
                ErrorMessage = "Account not found.",
                HttpStatusCode = 404
            };
        }
        return new ApiResponse<AccountResponse>
        {
            Result = new AccountResponse
            {
                AccountId = account.AccountNumber,
                Balance = account.Balance,
                Name = account.Name,
            }
        };
    }

    public ApiResponse<BalanceResponse> MakeDeposit(TransactionRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        if (account is null)
        {
            return new ApiResponse<BalanceResponse>
            {
                ErrorMessage = "Account not found.",
                HttpStatusCode = 404
            };
        }
        account.Balance += request.Amount;
        _context.SaveChanges();
        
        return new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  account.Balance,
            }
        };
    }

    public ApiResponse<BalanceResponse> MakeWithdraw(TransactionRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        if (account is null)
        {
            return new ApiResponse<BalanceResponse>
            {
                ErrorMessage = "Account not found.",
                HttpStatusCode = 404
            };
        }
        account.Balance -= request.Amount;
        _context.SaveChanges();
        
        return new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  account.Balance,
            }
        };
    }

    public ApiResponse<BalanceResponse> MakeTransfer(TransactionRequest request)
    {
        var sender = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.SenderAccId);
        var receiver = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.ReceiverAccId);
        if (sender is null || receiver is null)
        {
            return new ApiResponse<BalanceResponse>
            {
                ErrorMessage = "Account not found.",
                HttpStatusCode = 404
            };
        }
        
        sender.Balance -= request.Amount;
        receiver.Balance += request.Amount;
        _context.SaveChanges();
        
        return new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance =  sender.Balance,
            }
        };
    }

    public ApiResponse<BalanceResponse> CheckBalance(AccountRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.AccountNumber == request.AccountId);
        if (account is null)
        {
            return new ApiResponse<BalanceResponse>
            {
                ErrorMessage = "Account not found.",
                HttpStatusCode = 404
            };
        }
        
        return new ApiResponse<BalanceResponse>
        {
            Result = new BalanceResponse
            {
                Balance = account.Balance,
            }
        };
    }
}