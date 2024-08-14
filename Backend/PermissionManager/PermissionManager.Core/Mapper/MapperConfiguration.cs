using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Mapper
{
    public class MapperConfiguration
    {
        public static void ConfigurationMapper( IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfiguration).Assembly );

        }
    }
}
