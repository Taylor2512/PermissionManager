using MediatR;

using PermissionManager.Core.Services.Request;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands
{
    public record RequestPermissionCommand(PermissionRequest Request) : IRequest;
}