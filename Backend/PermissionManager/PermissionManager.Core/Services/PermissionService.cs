using AutoMapper;

using Elastic.Clients.Elasticsearch;

using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;
using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ElasticsearchClient _elasticClient;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, ElasticsearchClient elasticClient, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        public async Task RequestPermissionAsync(PermissionRequest request)
        {
            var permission = _mapper.Map<Permission>(request);
            _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.CompleteAsync();
            await IndexPermissionInElasticSearch(permission);
        }

        public async Task ModifyPermissionAsync( int id, PermissionRequest request)
        {
            var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(id);
            if (existingPermission != null)

            {
                var existingPermissionpath = _mapper.Map(request,existingPermission);
                await _unitOfWork.CompleteAsync();
                await IndexPermissionInElasticSearch(existingPermissionpath);
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsAsync()
        {
            var permissions = await _unitOfWork.Permissions.GetPermissionTypesWithPermissionsAsync();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        private async Task IndexPermissionInElasticSearch(Permission permission)
        {
           await _elasticClient.IndexAsync(permission, idx => idx.Index("permissions"));
        }
    }
}