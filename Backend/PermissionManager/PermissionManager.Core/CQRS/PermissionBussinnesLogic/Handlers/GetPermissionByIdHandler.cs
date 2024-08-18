using AutoMapper;

using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Exceptions;
using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IQueryPermissionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionByIdHandler(IQueryPermissionUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery query, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Permissions.GetByIWithTypesdAsync(query.Id);
            if (permission == null)
            {
                throw new NotFoundException($"Permission with ID {query.Id} not found.");
            }
            return _mapper.Map<PermissionDto>(permission);
        }
    }
}