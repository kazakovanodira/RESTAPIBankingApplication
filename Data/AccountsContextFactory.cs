using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RESTAPIBankingApplication.Data;

public class AccountsContextFactory : IDesignTimeDbContextFactory<AccountsContext>
{
    public AccountsContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory()));

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string is missing!");
        }
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 42)));
        Console.WriteLine("Loaded connection string: " + connectionString);
        return new AccountsContext(optionsBuilder.Options);
    }
}

