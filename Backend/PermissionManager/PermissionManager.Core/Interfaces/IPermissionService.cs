using PermissionManager.Core.Services.Dtos;
using PermissionManager.Core.Services.Request;

namespace PermissionManager.Core.Interfaces
{
    public interface IPermissionService
    {
        Task RequestPermissionAsync(PermissionRequest request);

        Task ModifyPermissionAsync(int id, PermissionRequest request);

        Task<IEnumerable<PermissionDto>> GetPermissionsAsync();

        Task<PermissionDto> GetPermissionByIdAsync(int id);
        Task DeletePermissionAsync(int id);
    }
}