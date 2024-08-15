using PermissionManager.Core.Models;

namespace PermissionManager.Core.Interfaces
{
    public interface IElasticSearchService
    {
        Task IndexPermissionAsync(Permission permission);

        Task DeletePermissionAsync(Permission permission);
    }
}