using PermissionManager.Core.Data.Repositories;
using PermissionManager.Core.Models;

namespace PermissionManager.Core.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IPermissionRepository Permissions { get; }
        public IPermissionTypeRepository PermissionTypes { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Permissions = new PermissionRepository(context);
            PermissionTypes = new PermissionTypeRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}