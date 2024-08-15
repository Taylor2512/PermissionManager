using Microsoft.Extensions.DependencyInjection;

namespace PermissionManager.Core.Mapper
{
    public class MapperConfiguration
    {
        public static void ConfigurationMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfiguration).Assembly);
        }
    }
}