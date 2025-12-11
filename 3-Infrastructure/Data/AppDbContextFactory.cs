using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace _3_Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString =
            "server=trolley.proxy.rlwy.net;port=13679;database=railway;user=root;password=BLwEHAqWJWKxvyjfUfDRkededWLSbEqo;";

        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40)));

        return new AppDbContext(optionsBuilder.Options);
    }
}