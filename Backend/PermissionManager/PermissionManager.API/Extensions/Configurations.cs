using Elastic.Clients.Elasticsearch;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;
using PermissionManager.Core.Services;

using System.Reflection;

namespace PermissionManager.API.Extensions
{
    public class Configurations
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPermissionsHandler).Assembly));
             
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            var client = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("permissions"));  // Aquí defines el índice predeterminado

            services.AddSingleton(client);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPermissionService, PermissionService>();
        }
    }
}
