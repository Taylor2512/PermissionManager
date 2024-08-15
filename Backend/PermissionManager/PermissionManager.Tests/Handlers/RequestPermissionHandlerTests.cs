using AutoMapper;
using MediatR;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;
using PermissionManager.Core.Services.Request;
 

using Telerik.JustMock;
 

namespace PermissionManager.Tests.Handlers
{
    [TestClass]
    public class RequestPermissionHandlerTests
    {
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly IElasticSearchService _mockElasticSearchService;
        private readonly IRequestHandler<RequestPermissionCommand> _handler;

        public RequestPermissionHandlerTests()
        {
            _mockUnitOfWork = Mock.Create<IUnitOfWork>();
            _mockMapper = Mock.Create<IMapper>();
            _mockElasticSearchService = Mock.Create<IElasticSearchService>();
            _handler = new RequestPermissionHandler(_mockUnitOfWork, _mockMapper, _mockElasticSearchService);
        }

        [TestMethod]
        public async Task Handle_RequestPermission_MapsAndSavesPermission()
        {
            var command = new RequestPermissionCommand(new PermissionRequest
            {
                FirstName = "John",
                LastName = "Doe",
                PermissionTypeId = 1
            });
            var permission = new Permission();
            Mock.Arrange(() => _mockMapper.Map<Permission>(command.Request)).Returns(permission);
            Mock.Arrange(() => _mockUnitOfWork.Permissions.Add(permission));
            Mock.Arrange(() => _mockUnitOfWork.CompleteAsync()).Returns(Task.FromResult(0));
            Mock.Arrange(() => _mockElasticSearchService.IndexPermissionAsync(permission)).Returns(Task.FromResult(0));

            await _handler.Handle(command, CancellationToken.None);

            Mock.Assert(() => _mockMapper.Map<Permission>(command.Request), Occurs.Once());
            Mock.Assert(() => _mockUnitOfWork.Permissions.Add(permission), Occurs.Once());
            Mock.Assert(() => _mockUnitOfWork.CompleteAsync(), Occurs.Once());
            Mock.Assert(() => _mockElasticSearchService.IndexPermissionAsync(permission), Occurs.Once());
        }
    }
}
