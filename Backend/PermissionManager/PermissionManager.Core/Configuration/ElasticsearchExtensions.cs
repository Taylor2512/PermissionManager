using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace PermissionManager.Core.Configuration
{
    public static class ElasticsearchExtensions
    {
        public static IServiceCollection ConfigureElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var uriElasticsearch = configuration["ElasticSearch:Uri"];
            var apikey = configuration["ElasticSearch:ApiKey"];
            var CloudID = configuration["ElasticSearch:CloudID"];
            if (!string.IsNullOrEmpty(CloudID) && !string.IsNullOrEmpty(apikey))
            {

                var settings = new ElasticsearchClientSettings(cloudId: CloudID, new ApiKey(apikey))
                    .Authentication(new ApiKey(apikey)).DefaultMappingFor<Permission>(i => i.IndexName("permissions").IdProperty(p => p.Id)).EnableDebugMode().PrettyJson().RequestTimeout(TimeSpan.FromMinutes(2));
                var client = new ElasticsearchClient(settings);
                services.AddSingleton(client);


            }
            else
            {
                throw new ArgumentException("Insertar las credenciales de elasticSearch");
            }

            return services;
        }
    }
}
