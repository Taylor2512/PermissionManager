using PermissionManager.Core.Data.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.UnitOfWork.Interfaces
{
    public interface ICommandPermissionUnitOfWork
    {
        IPermissionWriteRepository Permissions { get; }
        IPermissionTypeWriteRepository PermissionType { get; }
 
        Task<int> CompleteAsync();
    }
}
