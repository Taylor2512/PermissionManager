using Telerik.JustMock;
using AutoMapper;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Exceptions;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services.Request;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Telerik.JustMock.Helpers;
using PermissionManager.Producter;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;

namespace PermissionManager.Tests.Handlers
{
    [TestClass]
    public class ModifyPermissionHandlerTests
    {
        private readonly ICommandPermissionUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly IProducerService _mockElasticSearchService;
        private readonly ModifyPermissionHandler _handler;

        public ModifyPermissionHandlerTests()
        {
            _mockUnitOfWork = Mock.Create<ICommandPermissionUnitOfWork>();
            _mockMapper = Mock.Create<IMapper>();
            _mockElasticSearchService = Mock.Create<IProducerService>();
            _handler = new ModifyPermissionHandler(_mockUnitOfWork, _mockMapper, _mockElasticSearchService);
        }

        [TestMethod]
        public async Task Handle_ModifyPermission_ThrowsNotFoundException_IfPermissionNotFound()
        {
            var command = new ModifyPermissionCommand(1, new PermissionRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                PermissionTypeId = 2
            });
            Mock.Arrange(() => _mockUnitOfWork.Permissions.GetByIdAsync(command.Id)).ReturnsAsync((Permission)null);

            var exception = await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.AreEqual($"Permission with ID {command.Id} not found.", exception.Message);
        }

        [TestMethod]
        public async Task Handle_ModifyPermission_UpdatesPermission()
        {
            // Arrange
            var command = new ModifyPermissionCommand(1, new PermissionRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                PermissionTypeId = 2
            });

            var permission = new Permission
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PermissionTypeId = 1,
                PermissionDate = new DateOnly(2024, 8, 15),
                PermissionType = new PermissionType
                {
                    Id = 1,
                    Description = "Admin",
                    Name = "Admin"
                }
            };

            // Mock GetByIdAsync to return the permission object
            Mock.Arrange(() => _mockUnitOfWork.Permissions.GetByIdAsync(command.Id))
                .ReturnsAsync(permission);

            // Mock CompleteAsync to return a Task<int> with a value of 1
            Mock.Arrange(() => _mockUnitOfWork.CompleteAsync())
                .Returns(Task.FromResult(1));

            // Mock IndexPermissionAsync to return a completed task
            //Mock.Arrange(() => _mockElasticSearchService.ProduceAsync( permission.GetType().Name, permission))
                //.Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Mock.Assert(() => _mockMapper.Map(command.Request, permission), Occurs.Once());
            Mock.Assert(() => _mockUnitOfWork.CompleteAsync(), Occurs.Once());
            //Mock.Assert(() => _mockElasticSearchService.ProduceAsync(permission.GetType().Name, permission), Occurs.Once());
        }

    }
}
