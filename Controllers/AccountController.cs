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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var account = _accountsService.CreateAccount(request);
        
        if (!account.IsSuccess)
        {
            return StatusCode(account.HttpStatusCode, account);
        }
        
        return CreatedAtAction(nameof(GetAccount), new { accountNumber = account.Result.AccountId }, account);
    }

    [HttpGet("{accountNumber}")]
    public IActionResult GetAccount(Guid accountNumber)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var account = _accountsService.GetAccount(new AccountRequest { AccountId = accountNumber });
        if (!account.IsSuccess)
        {
            return StatusCode(account.HttpStatusCode, account);
        }
        return Ok(account);
    }
    
    [HttpPost("{accountNumber}/deposits")]
    public IActionResult MakeDeposit(Guid accountNumber,[FromBody] TransactionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var account = _accountsService.MakeDeposit(new TransactionRequest()
        {
            SenderAccId = accountNumber,
            Amount = request.Amount,
        });
        
        if (!account.IsSuccess)
        {
            return StatusCode(account.HttpStatusCode, account);
        }
        return Ok(account);
    }
    
    [HttpPost("{accountNumber}/withdrawals")]
    public IActionResult MakeWithdrawal(Guid accountNumber,[FromBody] TransactionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var account = _accountsService.MakeWithdraw(new TransactionRequest()
        {
            SenderAccId = accountNumber,
            Amount = request.Amount,
        });
        
        if (!account.IsSuccess)
        {
            return StatusCode(account.HttpStatusCode, account);
        }
        return Ok(account);
    }
    
    [HttpPost("transfer")]
    public IActionResult MakeTransfer([FromBody] TransactionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var account = _accountsService.MakeTransfer(new TransactionRequest()
        {
            SenderAccId = request.SenderAccId,
            ReceiverAccId = request.ReceiverAccId,
            Amount = request.Amount,
        });
        
        if (!account.IsSuccess)
        {
            return StatusCode(account.HttpStatusCode, account);
        }
        return Ok(account);
    }
}