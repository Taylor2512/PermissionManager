using Elastic.Clients.Elasticsearch;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Data.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionTypeReadRepository : RepositoryRead<PermissionType>, IPermissionTypeReadRepository
    {
        public PermissionTypeReadRepository(ElasticsearchClient client, ILogger<ElasticsearchRepository<PermissionType>> logger) : base(client, logger)
        {
        }
    }
}
