using AutoMapper;

using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IQueryPermissionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionsHandler(IQueryPermissionUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<PermissionDto> permissions = await _unitOfWork.Permissions.GetPermissionTypesWithPermissionsAsync();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }
    }
}