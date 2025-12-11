using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TalentoPlus.Web.Data;

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
{
    public AppIdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();

        var connectionString =
            "server=trolley.proxy.rlwy.net;port=13679;database=railway;user=root;password=BLwEHAqWJWKxvyjfUfDRkededWLSbEqo;";

        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40)));

        return new AppIdentityDbContext(optionsBuilder.Options);
    }
}