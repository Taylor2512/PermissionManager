using PermissionManager.Shared.Models;

namespace PermissionManager.Core.Data.Repositories.Interfaces
{
    public interface IPermissionWriteRepository : IRepositoryWrite<Permission>
    {
        Task<Permission?> GetByIWithTypesdAsync(int id);

        Task<IEnumerable<Permission>> GetPermissionTypesWithPermissionsAsync();
    }
}