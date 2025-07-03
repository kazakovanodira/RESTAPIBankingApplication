using Microsoft.AspNetCore.Mvc;
using RESTAPIBankingApplication.Interface;
using RESTAPIBankingApplication.Models.Requests;

namespace RESTAPIBankingApplication.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountsService;
    public AccountsController(IAccountsService service)
    {
        _accountsService = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAccount([FromBody] CreateAccountRequest request)
    {
        var account = _accountsService.CreateAccount(request);
        return Ok(account);
    }

    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetAccount(Guid accountNumber)
    {
        var account = _accountsService.GetAccount(new AccountRequest { AccountId = accountNumber });
        if (account is null)
        {
            return NotFound();
        }
        return Ok(account);
    }
    
    
    
    
}