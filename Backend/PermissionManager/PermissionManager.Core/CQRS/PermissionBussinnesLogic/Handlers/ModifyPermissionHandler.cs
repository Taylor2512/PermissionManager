using AutoMapper;

using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Exceptions;
using PermissionManager.Core.Interfaces;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService _elasticSearchService;

        public ModifyPermissionHandler(IUnitOfWork unitOfWork, IMapper mapper, IElasticSearchService elasticSearchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticSearchService = elasticSearchService;
        }

        public async Task Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);
            if (existingPermission == null)
            {
                throw new NotFoundException($"Permission with ID {request.Id} not found.");
            }
            _mapper.Map(request.Request, existingPermission);
            await _unitOfWork.CompleteAsync();
            await _elasticSearchService.IndexPermissionAsync(existingPermission);
        }
    }
}