using AutoMapper;

using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Exceptions;
using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly ICommandPermissionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public ModifyPermissionHandler(ICommandPermissionUnitOfWork unitOfWork, IMapper mapper, IProducerService producerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _producerService = producerService;
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
            var eventMessage = new PermissionEvent<Permission>
            {
                OperationType = "Update",
                EventData = existingPermission
            };

            await _producerService.ProduceAsync(nameof(Permission), existingPermission.Id.ToString(), eventMessage);

        }
    }
}