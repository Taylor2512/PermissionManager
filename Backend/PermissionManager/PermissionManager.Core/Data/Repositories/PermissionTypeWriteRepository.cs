using PermissionManager.Core.Data.Repositories.Interfaces;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionTypeWriteRepository : RepositoryWrite<PermissionType>, IPermissionTypeWriteRepository
    {
        public PermissionTypeWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}