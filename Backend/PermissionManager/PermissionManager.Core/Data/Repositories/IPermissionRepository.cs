using PermissionManager.Core.Models;

namespace PermissionManager.Core.Data.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission> GetPermissionWithTypeAsync(int id);
        Task<IEnumerable<Permission>> GetPermissionTypesWithPermissionsAsync();
    }
}