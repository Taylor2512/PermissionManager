using PermissionManager.Core.Data.Repositories;
using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.UnitOfWork
{
    public class QueryPermissionUnitOfWork : IQueryPermissionUnitOfWork
    {

        public IPermissionReadRepository Permissions { get; }
        public IPermissionTypeReadRepository PermissionTypeReadRepository { get; }

        public QueryPermissionUnitOfWork(
            IPermissionReadRepository permissionReadRepository,
            IPermissionTypeReadRepository permissionTypeReadRepository)
        {
            Permissions = permissionReadRepository;
            PermissionTypeReadRepository = permissionTypeReadRepository;
        }

    }

}
