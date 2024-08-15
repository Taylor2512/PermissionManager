using AutoMapper;

using MediatR;

using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;

namespace PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers
{
    public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService _elasticSearchService;

        public RequestPermissionHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IElasticSearchService elasticSearchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticSearchService = elasticSearchService;
        }

        public async Task Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<Permission>(request.Request);
            _unitOfWork.Permissions.Add(permission);
            await _unitOfWork.CompleteAsync();
            await _elasticSearchService.IndexPermissionAsync(permission);
        }
    }
}