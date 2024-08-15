using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services.Dtos;
using PermissionManager.Core.Services.Request;

namespace PermissionManager.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMediator _mediator;

        public PermissionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RequestPermissionAsync(PermissionRequest request)
        {
            return _mediator.Send(new RequestPermissionCommand(request));
        }

        public Task ModifyPermissionAsync(int id, PermissionRequest request)
        {
            return _mediator.Send(new ModifyPermissionCommand(id, request));
        }

        public Task<IEnumerable<PermissionDto>> GetPermissionsAsync()
        {
            return _mediator.Send(new GetPermissionsQuery());
        }

        public Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            return _mediator.Send(new GetPermissionByIdQuery(id));
        }

        public Task DeletePermissionAsync(int id)
        {
            return _mediator.Send(new DeletePermissionCommand(id));
        }
    }
}