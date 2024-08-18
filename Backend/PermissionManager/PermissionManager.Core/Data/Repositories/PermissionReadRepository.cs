using AutoMapper.Configuration.Annotations;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Logging;

using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Core.Services.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionReadRepository : RepositoryRead<Permission>, IPermissionReadRepository
    {
        public PermissionReadRepository(ElasticsearchClient client, ILogger<ElasticsearchRepository<Permission>> logger) : base(client, logger)
        {
        }

        public async Task<Permission?> GetByIWithTypesdAsync(int id)
        {
            return await GetById(id);
        }

        public async Task<IEnumerable<Permission>> GetPermissionTypesWithPermissionsAsync()
        {
            var response = await _client.SearchAsync<Permission>(e=>e.Index(IndexName));
            if (response.IsSuccess())
            {
                return response.Documents;
            }
            return null;
        }
    }
}
