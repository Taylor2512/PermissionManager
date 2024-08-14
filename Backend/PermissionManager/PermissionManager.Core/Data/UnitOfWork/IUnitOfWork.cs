using PermissionManager.Core.Data.Repositories;

namespace PermissionManager.Core.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IPermissionTypeRepository PermissionTypes { get; }

        Task<int> CompleteAsync();
    }
}