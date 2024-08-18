using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Data;
using PermissionManager.Core.Interfaces;

using PermissionManager.Core.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Core.Data.Repositories;

namespace PermissionManager.Core.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPermissionsHandler).Assembly));
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICommandPermissionUnitOfWork, CommandPermissionUnitOfWork>();
            services.AddScoped<IQueryPermissionUnitOfWork, QueryPermissionUnitOfWork>();
            services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
            services.AddScoped<IPermissionWriteRepository, PermissionWriteRepository>();
            services.AddScoped<IPermissionTypeReadRepository, PermissionTypeReadRepository>();
            services.AddScoped<IPermissionTypeWriteRepository, PermissionTypeWriteRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.ConfigureElasticSearch(configuration);
            return services;
        }
    }
}
