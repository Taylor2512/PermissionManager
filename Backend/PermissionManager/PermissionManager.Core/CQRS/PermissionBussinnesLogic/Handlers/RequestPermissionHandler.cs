using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore.Diagnostics;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand>
    {
        private readonly ICommandPermissionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public RequestPermissionHandler(ICommandPermissionUnitOfWork unitOfWork, IMapper mapper, IProducerService producerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _producerService = producerService;
        }

        public async Task Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<Permission>(request.Request);
            _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.CompleteAsync();
            var eventMessage = new PermissionEvent<Permission>
            {
                OperationType = "Create",
                EventData = permission
            };

            await _producerService.ProduceAsync(nameof(Permission), permission.Id.ToString(), eventMessage);
        }
    }
}