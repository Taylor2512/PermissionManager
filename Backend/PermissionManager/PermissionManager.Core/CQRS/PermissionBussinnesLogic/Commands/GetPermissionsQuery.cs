using MediatR;

using PermissionManager.Core.Services.Dtos;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands
{
    public record GetPermissionsQuery() : IRequest<IEnumerable<PermissionDto>>;
}