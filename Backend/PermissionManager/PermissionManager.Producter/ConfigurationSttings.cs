using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Producter
{
    public static class ConfigurationSttings
    {
        public static IServiceCollection AddProducer(this IServiceCollection service)
        {
            service.AddScoped<IProducerService,ProducerService>();
            return service;
        }
    }
}
