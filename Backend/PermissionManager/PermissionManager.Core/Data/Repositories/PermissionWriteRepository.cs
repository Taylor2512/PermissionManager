using Microsoft.EntityFrameworkCore;

using PermissionManager.Core.Data.Repositories.Interfaces;

namespace PermissionManager.Core.Data.Repositories
{
    public class PermissionWriteRepository : RepositoryWrite<Permission>, IPermissionWriteRepository
    {
        public PermissionWriteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Permission>> GetPermissionTypesWithPermissionsAsync()
        {
            return await _context.Permissions
            .Include(p => p.PermissionType)
                .ToListAsync();
        }

        public async Task<Permission?> GetByIWithTypesdAsync(int id)
        {
            return await _context.Permissions
           .Include(p => p.PermissionType)
           .Where(e => e.Id == id)
               .FirstOrDefaultAsync();
        }
    }
}