using PermissionManager.Core.Services.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.Repositories.Interfaces
{
    public interface IPermissionReadRepository : IRepositoryRead<Permission>
    {
        Task<PermissionDto?> GetByIWithTypesdAsync(int id);
        Task<IEnumerable<PermissionDto>> GetPermissionTypesWithPermissionsAsync();
    }
}
