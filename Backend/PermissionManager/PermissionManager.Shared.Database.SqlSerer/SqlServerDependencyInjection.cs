using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Shared.Databases.Sql
{
    public static IServiceCollection AddMySqlDbContext<T>(this IServiceCollection serviceCollection,
         Func<IServiceProvider, Task<string>> connectionString)
         where T : DbContext
    {
        return serviceCollection.AddDbContext<T>((serviceProvider, builder) =>
        {
            builder.UseMySQL(connectionString.Invoke(serviceProvider).Result);
        });
    }

    public static IServiceCollection AddMysqlHealthCheck(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
    {
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        string mySqlConnectionString = connectionString.Invoke(serviceProvider).Result;
        serviceCollection.AddHealthChecks().AddMySql(mySqlConnectionString);
        return serviceCollection;
    }
}
