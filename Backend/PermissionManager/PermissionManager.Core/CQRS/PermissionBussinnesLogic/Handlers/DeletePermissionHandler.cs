using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Exceptions;
using PermissionManager.Core.Interfaces;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService _elasticSearchService;

        public DeletePermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService elasticSearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticSearchService = elasticSearchService;
        }

        public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);
            if (permission == null)
            {
                throw new NotFoundException($"Permission with ID {request.Id} not found.");
            }
            _unitOfWork.Permissions.Remove(permission);
            await _unitOfWork.CompleteAsync();
            await _elasticSearchService.DeletePermissionAsync(permission);
        }
    }
}