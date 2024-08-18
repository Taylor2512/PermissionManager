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
            var permision= await GetById(id);
            if(permision == null)
            {
                return null;
            }
            var permisionType = await _client.GetAsync<PermissionType>(permision.PermissionTypeId, idx => idx.Index(nameof(PermissionType).ToLower()+"s"));
            if (permisionType.IsValidResponse)
            {
                permision.PermissionType = permisionType.Source;
            }
            return permision;
        }

        public async Task<IEnumerable<Permission>> GetPermissionTypesWithPermissionsAsync()
        {
            var permisionType = await _client.SearchAsync<PermissionType>(idx => idx.Index(nameof(PermissionType).ToLower() + "s"));

            var response = await _client.SearchAsync<Permission>(e=>e.Index(IndexName));
            if (response.IsSuccess())
            {
                if (permisionType.IsValidResponse) {
                    foreach (var item in response.Documents)
                    {
                        item.PermissionType = permisionType.Documents.FirstOrDefault(x => x.Id == item.PermissionTypeId);
                    }
                }
                return response.Documents;
            }
            return null;
        }
    }
}
