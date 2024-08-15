using MediatR;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands
{
    public record DeletePermissionCommand(int Id) : IRequest;
}