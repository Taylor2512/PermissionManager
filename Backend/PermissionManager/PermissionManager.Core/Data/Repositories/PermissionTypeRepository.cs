using PermissionManager.Core.Models;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}