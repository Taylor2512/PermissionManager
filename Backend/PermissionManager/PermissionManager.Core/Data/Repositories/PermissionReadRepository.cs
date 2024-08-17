using AutoMapper.Configuration.Annotations;

using Elastic.Clients.Elasticsearch;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionReadRepository : RepositoryRead<Permission>, IPermissionReadRepository
    {
        public PermissionReadRepository(ElasticsearchClient client, ILogger<ElasticsearchRepository<Permission>> logger) : base(client, logger)
        {
        }

        public Task<PermissionDto?> GetByIWithTypesdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionDto>> GetPermissionTypesWithPermissionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
