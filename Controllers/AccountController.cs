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

    /// <summary>
    /// Creates a new bank account with the specified account holder name.
    /// </summary>
    /// <param name="request">Contains the account holder's name.</param>
    /// <returns>The created account details or an error response.</returns>
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
    
    /// <summary>
    /// Retrieves account details by account number.
    /// </summary>
    /// <param name="accountNumber">The unique identifier of the account.</param>
    /// <returns>The account details or an error response if not found.</returns>
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
    
    /// <summary>
    /// Makes a deposit into the specified account.
    /// </summary>
    /// <param name="accountNumber">The account number to deposit into.</param>
    /// <param name="request">The deposit request containing the deposit amount.</param>
    /// <returns>The updated balance or an error response.</returns>
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
    
    /// <summary>
    /// Makes a withdrawal from the specified account.
    /// </summary>
    /// <param name="accountNumber">The account number to withdraw from.</param>
    /// <param name="request">The withdrawal request containing the withdrawal amount.</param>
    /// <returns>The updated balance or an error response.</returns>
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
    
    /// <summary>
    /// Transfers funds between two accounts.
    /// </summary>
    /// <param name="request">The transfer request containing sender, receiver, and transfer amount.</param>
    /// <returns>The updated sender balance or an error response.</returns>
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