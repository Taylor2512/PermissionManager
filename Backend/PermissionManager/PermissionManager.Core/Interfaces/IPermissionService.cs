using PermissionManager.Core.Models;
using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.Interfaces
{
    public interface IPermissionService
    {
        Task RequestPermissionAsync(PermissionRequest request);

        Task ModifyPermissionAsync(int id,PermissionRequest request);

        Task<IEnumerable<PermissionDto>> GetPermissionsAsync();
    }
}