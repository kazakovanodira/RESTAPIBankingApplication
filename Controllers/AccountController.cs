using Microsoft.AspNetCore.Mvc;
using RESTAPIBankingApplication.Models;

namespace RESTAPIBankingApplication.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    static private List<Account> accounts = new List<Account>
    {
        new Account()
        {
            AccountNumber = Guid.NewGuid(),
            Balance = 100,
            Name = "nadi"
        },

        new Account()
        {
            AccountNumber = Guid.NewGuid(),
            Balance = 100,
            Name = "stan"
        }
    };
    
    [HttpGet("{name}")]
    public ActionResult<Account> GetAccount(string name)
    {
        var account = accounts.FirstOrDefault(x => x.Name  == name);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpPost("{newName}")]
    public ActionResult<Account> CreateNewAccount(string newName)
    {
        if (newName == null)
        {
            return BadRequest();
        }

        var newAccount = new Account()
        {
            AccountNumber = Guid.NewGuid(),
            Balance = 0,
            Name = newName
        };
        
        accounts.Add(newAccount);
        return CreatedAtAction(nameof(GetAccount), new { name = newName }, accounts.Last());
    }
    
    
}