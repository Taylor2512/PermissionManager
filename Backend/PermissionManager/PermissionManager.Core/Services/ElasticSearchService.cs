using Elastic.Clients.Elasticsearch;

using PermissionManager.Core.Interfaces;

namespace PermissionManager.Core.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticsearchClient _elasticClient;

        public ElasticSearchService(ElasticsearchClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task DeletePermissionAsync(Permission permission)
        {
            var response = await _elasticClient.DeleteAsync<Permission>(permission.Id, idx => idx.Index("permissions"));
            if (!response.IsValidResponse)
            {
                throw new Exception("Failed to delete permission from Elasticsearch");
            }
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            var response = await _elasticClient.IndexAsync(permission, idx => idx.Index("permissions"));

            if (!response.IsValidResponse)
            {
                throw new Exception("Failed to index permission in ElasticSearch");
            }
        }
    }
}