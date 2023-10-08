using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Infrastructure.CouperfectDatabase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CouperfectServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCouperfectDb(this IServiceCollection services)
    {
        services.AddDbContext<CouperfectDbContext>(opt =>
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "couperfect.db",
                Cache = SqliteCacheMode.Shared
            };
            var sqliteConnection = new SqliteConnection(connectionStringBuilder.ToString());
            sqliteConnection.Open();
            opt.UseSqlite(sqliteConnection);
        });
        services.AddScoped(sp => sp.GetRequiredService<CouperfectDbContext>() as ICouperfectDbUnitOfWork);

        return services;
    }
}
