using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.UnitOfWork
{
    public class CommandPermissionUnitOfWork : ICommandPermissionUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;


        public IPermissionWriteRepository Permissions { get; }

        public IPermissionTypeWriteRepository PermissionType { get; }

        public CommandPermissionUnitOfWork(
            ApplicationDbContext context,
            IPermissionWriteRepository permissionWriteRepository,
            IPermissionTypeWriteRepository permissionTypeWriteRepository)
        {
            _context = context;
            Permissions = permissionWriteRepository;
            PermissionType = permissionTypeWriteRepository;
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
