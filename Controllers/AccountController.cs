using Microsoft.AspNetCore.Mvc;
using RESTAPIBankingApplication.Interface;
using RESTAPIBankingApplication.Models;
using RESTAPIBankingApplication.Models.Requests;
using RESTAPIBankingApplication.Services;

namespace RESTAPIBankingApplication.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountsService = new AccountsServices();
    public AccountRequest AccountRequest = new AccountRequest();
    public CreateAccountRequest CreateAccountRequest = new CreateAccountRequest();
    public TransactionRequest TransactionRequest = new TransactionRequest();
    
    
    [HttpPost]
    public ActionResult<IActionResult> CreateNewAccount([FromBody]string newName)
    {
        CreateAccountRequest.Name = newName;
        return _accountsService.CreateAccount(CreateAccountRequest);
        return CreatedAtAction(nameof(CreateNewAccount), new { name = newName }, accounts.Last());
    }

    [HttpGet("{accountNumber}")]
    public ActionResult<Account> GetAccount(int accountNumber)
    {
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        if (account is null)
        {
            return NotFound();
        }
        return account;
    }
}