using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Exceptions;
using PermissionManager.Core.Interfaces;
using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand>
    {
        private readonly ICommandPermissionUnitOfWork _unitOfWork;
        private readonly IProducerService _producterServices;

        public DeletePermissionHandler(ICommandPermissionUnitOfWork unitOfWork, IProducerService producterServices)
        {
            _unitOfWork = unitOfWork;
            _producterServices = producterServices;
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
            var eventMessage = new PermissionEvent<Permission>
            {
                OperationType = "Delete",
                EventData = permission
            };

            await _producterServices.ProduceAsync(nameof(Permission), permission.Id.ToString(), eventMessage);
        }
    }
}