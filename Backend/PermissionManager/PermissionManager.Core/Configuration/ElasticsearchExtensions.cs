using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionManager.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionManager.Core.Configuration
{
    public static class ElasticsearchExtensions
    {
        public static IServiceCollection ConfigureElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticConfig = new ElasticSearchAuthConfig();
            configuration.GetSection(nameof(ElasticSearchAuthConfig)).Bind(elasticConfig);

            // Validar la configuración
            Validator.ValidateObject(elasticConfig, new ValidationContext(elasticConfig), validateAllProperties: true);

            // Comprobar si la configuración es válida
            if (string.IsNullOrEmpty(elasticConfig.Url) && string.IsNullOrEmpty(elasticConfig.CloudID))
            {
                throw new ArgumentException("El URI de Elasticsearch o el CloudID deben estar configurados.");
            }

            ElasticsearchClientSettings settings;

            // Configuración de Elasticsearch basada en CloudID
            if (!string.IsNullOrEmpty(elasticConfig.CloudID) && !string.IsNullOrEmpty(elasticConfig.ApiKey))
            {
                settings = new ElasticsearchClientSettings(cloudId: elasticConfig.CloudID, new ApiKey(elasticConfig.ApiKey))
                    .DefaultMappingFor<Permission>(i => i.IndexName("permissions").IdProperty(p => p.Id))
                    .EnableDebugMode()
                    .PrettyJson()
                    .RequestTimeout(TimeSpan.FromMinutes(2));
            }
            // Configuración de Elasticsearch basada en URL y ApiKey
            else if (!string.IsNullOrEmpty(elasticConfig.Url) && !string.IsNullOrEmpty(elasticConfig.ApiKey))
            {
                settings = new ElasticsearchClientSettings(new Uri(elasticConfig.Url))
                    .Authentication(new ApiKey(elasticConfig.ApiKey))
                    .DefaultMappingFor<Permission>(i => i.IndexName("permissions").IdProperty(p => p.Id))
                    .EnableDebugMode()
                    .PrettyJson()
                    .RequestTimeout(TimeSpan.FromMinutes(2));
            }
            // Configuración de Elasticsearch basada en URL y credenciales básicas
            else if (!string.IsNullOrEmpty(elasticConfig.Url) && !string.IsNullOrEmpty(elasticConfig.Username) && !string.IsNullOrEmpty(elasticConfig.Password))
            {
                settings = new ElasticsearchClientSettings(new Uri(elasticConfig.Url))
                    .Authentication(new BasicAuthentication(elasticConfig.Username, elasticConfig.Password))
                    .DefaultMappingFor<Permission>(i => i.IndexName("permissions").IdProperty(p => p.Id))
                    .EnableDebugMode()
                    .PrettyJson()
                        .ServerCertificateValidationCallback(CertificateValidations.AllowAll)

                    .RequestTimeout(TimeSpan.FromMinutes(2));
            }
            else
            {
                throw new ArgumentException("Se deben proporcionar credenciales de Elasticsearch válidas.");
            }

            // Registrar el cliente de Elasticsearch como un servicio singleton
            var client = new ElasticsearchClient(settings);
            services.AddSingleton(client);

            return services;
        }
    }
}
