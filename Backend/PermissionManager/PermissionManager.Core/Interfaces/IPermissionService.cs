using PermissionManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Interfaces
{
    public interface IPermissionService
    {
        Task RequestPermissionAsync(Permission permission);
        Task ModifyPermissionAsync(Permission permission);
        Task<IEnumerable<Permission>> GetPermissionsAsync();
    }
}
