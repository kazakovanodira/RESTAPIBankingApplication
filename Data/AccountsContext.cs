using Microsoft.EntityFrameworkCore;
using RESTAPIBankingApplication.Models;

namespace RESTAPIBankingApplication.Data;

public class AccountsContext : DbContext
{
    public AccountsContext(DbContextOptions<AccountsContext> options):base(options) { }
    
    
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();
    }
}