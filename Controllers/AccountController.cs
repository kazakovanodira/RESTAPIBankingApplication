using Microsoft.AspNetCore.Mvc;
using RESTAPIBankingApplication.Interface;
using RESTAPIBankingApplication.Models;
using RESTAPIBankingApplication.Models.Requests;
using RESTAPIBankingApplication.Models.Responses;
using RESTAPIBankingApplication.Services;

namespace RESTAPIBankingApplication.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountsService;
    public AccountRequest AccountRequest = new AccountRequest();
    public CreateAccountRequest CreateAccountRequest = new CreateAccountRequest();
    public TransactionRequest TransactionRequest = new TransactionRequest();
    public AccountsController(IAccountsService service)
    {
        _accountsService = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAccount([FromBody] CreateAccountRequest request)
    {
        var acc = _accountsService.CreateAccount(request);
        return Ok(acc);
    }

    /*[HttpGet("{accountNumber}")]
    public ActionResult<Account> GetAccount(int accountNumber)
    {
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        if (account is null)
        {
            return NotFound();
        }
        return account;
    }*/
}