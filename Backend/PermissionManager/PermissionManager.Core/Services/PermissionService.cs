using Elastic.Clients.Elasticsearch;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ElasticsearchClient _elasticClient;

        public PermissionService(IUnitOfWork unitOfWork, ElasticsearchClient elasticClient)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
        }

        public async Task RequestPermissionAsync(Permission permission)
        {
            _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.CompleteAsync();
            await IndexPermissionInElasticSearch(permission);
        }

        public async Task ModifyPermissionAsync(Permission permission)
        {
            var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(permission.Id);
            if (existingPermission != null)
            {
                existingPermission.FirstName = permission.FirstName;
                existingPermission.LastName = permission.LastName;
                existingPermission.PermissionDate = permission.PermissionDate;
                existingPermission.PermissionTypeId = permission.PermissionTypeId;
                await _unitOfWork.CompleteAsync();
                await IndexPermissionInElasticSearch(existingPermission);
            }
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _unitOfWork.Permissions.GetAllAsync();
        }

        private async Task IndexPermissionInElasticSearch(Permission permission)
        {
            await _elasticClient.IndexAsync(permission);
        }
    }
}
