using Telerik.JustMock;
using AutoMapper;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;

using PermissionManager.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;

namespace PermissionManager.Tests.Handlers
{
    [TestClass]

    public class GetPermissionByIdHandlerTests
    {
        private readonly IQueryPermissionUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly GetPermissionByIdHandler _handler;

        public GetPermissionByIdHandlerTests()
        {
            _mockUnitOfWork = Mock.Create<IQueryPermissionUnitOfWork>();
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
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionTypeId = 1,
                PermissionDate = DateTime.UtcNow,
                PermissionType = new PermissionType { Id = 1, Description = "Admin", Name = "Admin" }
            };
            var permissionDto = new PermissionDto
            {
                Id = 1,
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionTypeName = "Admin",
                PermissionDate = DateTime.UtcNow,
                PermissionType = new PermissionTypeDto
                {
                    Id = 1,
                    Description = "Admin",
                    Name = "Admin"
                }
            };

            // Configurar mocks
            //Mock.Arrange(() => _mockUnitOfWork.Permissions.GetByIWithTypesdAsync(id)).ReturnsAsync(permission);
            Mock.Arrange(() => _mockMapper.Map<PermissionDto>(permission)).Returns(permissionDto);

            // Act
            var query = new GetPermissionByIdQuery(id);
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert - Comparar propiedades en lugar de instancias completas
            Assert.IsNotNull(result);
            Assert.AreEqual(permissionDto.Id, result.Id);
            Assert.AreEqual(permissionDto.EmployeeForename, result.EmployeeForename);
            Assert.AreEqual(permissionDto.EmployeeSurname, result.EmployeeSurname);
            Assert.AreEqual(permissionDto.PermissionTypeName, result.PermissionTypeName);
            Assert.AreEqual(permissionDto.PermissionDate, result.PermissionDate);
            Assert.AreEqual(permissionDto.PermissionType.Id, result.PermissionType.Id);
            Assert.AreEqual(permissionDto.PermissionType.Description, result.PermissionType.Description);
            Assert.AreEqual(permissionDto.PermissionType.Name, result.PermissionType.Name);
        }
       
    }
}
