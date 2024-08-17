using Elastic.Clients.Elasticsearch;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;

using System.Reflection;
using PermissionManager.Producter;
using PermissionManager.Core.Data;
using PermissionManager.Core.Configuration;

namespace PermissionManager.API.Extensions
{
    public class Configurations
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddConfigureServices(configuration);
            services.AddProducer();
        }
    }
}
