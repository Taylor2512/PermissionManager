using Telerik.JustMock;
using AutoMapper;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;
using PermissionManager.Core.Data.UnitOfWork;
using PermissionManager.Core.Models;
using PermissionManager.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PermissionManager.Tests.Handlers
{
    [TestClass]

    public class GetPermissionByIdHandlerTests
    {
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly GetPermissionByIdHandler _handler;

        public GetPermissionByIdHandlerTests()
        {
            _mockUnitOfWork = Mock.Create<IUnitOfWork>();
            _mockMapper = Mock.Create<IMapper>();
            _handler = new GetPermissionByIdHandler(_mockUnitOfWork, _mockMapper);
        }
        [TestMethod]
        public async Task Handle_GetPermissionById_ReturnsPermissionDto()
        {
            // Arrange
            var id = 1;
            var permission = new Permission
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PermissionTypeId = 1,
                PermissionDate = new DateOnly(2024, 8, 15),
                PermissionType = new PermissionType { Id = 1, Description = "Admin", Name = "Admin" }
            };
            var permissionDto = new PermissionDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PermissionTypeName = "Admin",
                PermissionDate = new DateOnly(2024, 8, 15),
                PermissionType = new PermissionTypeDto
                {
                    Id = 1,
                    Description = "Admin",
                    Name = "Admin"
                }
            };

            // Configurar mocks
            Mock.Arrange(() => _mockUnitOfWork.Permissions.GetByIWithTypesdAsync(id)).ReturnsAsync(permission);
            Mock.Arrange(() => _mockMapper.Map<PermissionDto>(permission)).Returns(permissionDto);

            // Act
            var query = new GetPermissionByIdQuery(id);
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert - Comparar propiedades en lugar de instancias completas
            Assert.IsNotNull(result);
            Assert.AreEqual(permissionDto.Id, result.Id);
            Assert.AreEqual(permissionDto.FirstName, result.FirstName);
            Assert.AreEqual(permissionDto.LastName, result.LastName);
            Assert.AreEqual(permissionDto.PermissionTypeName, result.PermissionTypeName);
            Assert.AreEqual(permissionDto.PermissionDate, result.PermissionDate);
            Assert.AreEqual(permissionDto.PermissionType.Id, result.PermissionType.Id);
            Assert.AreEqual(permissionDto.PermissionType.Description, result.PermissionType.Description);
            Assert.AreEqual(permissionDto.PermissionType.Name, result.PermissionType.Name);
        }
       
    }
}
