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
    public IActionResult CreateNewAccount([FromBody] CreateAccountRequest request)
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
    
    [HttpGet("{accountNumber}/deposits")]
    public async Task<IActionResult> MakeDeposit(Guid accountNumber,[FromBody] TransactionRequest request)
    {
        var account = _accountsService.MakeDeposit(new TransactionRequest()
        {
            SenderAccId = accountNumber,
            Amount = request.Amount,
        });
        
        if (account is null)
        {
            return NotFound();
        }
        return Ok(account);
    }
    
    [HttpGet("{accountNumber}/withdrawals")]
    public async Task<IActionResult> MakeWithdrawal(Guid accountNumber,[FromBody] TransactionRequest request)
    {
        var account = _accountsService.MakeWithdraw(new TransactionRequest()
        {
            SenderAccId = accountNumber,
            Amount = request.Amount,
        });
        
        if (account is null)
        {
            return NotFound();
        }
        return Ok(account);
    }
    
    [HttpGet("transfer")]
    public async Task<IActionResult> MakeTransfer([FromBody] TransactionRequest request)
    {
        var account = _accountsService.MakeWithdraw(new TransactionRequest()
        {
            SenderAccId = request.SenderAccId,
            ReceiverAccId = request.ReceiverAccId,
            Amount = request.Amount,
        });
        
        if (account is null)
        {
            return NotFound();
        }
        return Ok(account);
    }
}