using PermissionManager.Core.Data.Repositories;
using PermissionManager.Core.Data.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.UnitOfWork.Interfaces
{
    public interface IQueryPermissionUnitOfWork
    {
        public IPermissionReadRepository Permissions { get; }
        public IPermissionTypeReadRepository PermissionTypeReadRepository { get; }
    }
}
