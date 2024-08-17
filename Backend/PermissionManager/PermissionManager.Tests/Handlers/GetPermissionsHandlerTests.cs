using Telerik.JustMock;
using AutoMapper;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Commands;
using PermissionManager.Core.CQRS.PermissionBussinnesLogic.Handlers;

using PermissionManager.Core.Services.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;

namespace PermissionManager.Tests.Handlers
{
    [TestClass]

    public class GetPermissionsHandlerTests
    {
        private readonly IQueryPermissionUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly GetPermissionsHandler _handler;

        public GetPermissionsHandlerTests()
        {
            _mockUnitOfWork = Mock.Create<IQueryPermissionUnitOfWork>();
            _mockMapper = Mock.Create<IMapper>();
            _handler = new GetPermissionsHandler(_mockUnitOfWork, _mockMapper);
        }

        [TestMethod]
        public async Task Handle_GetPermissions_ReturnsListOfPermissionDtos()
        {
            var query = new GetPermissionsQuery();
            IEnumerable<Permission> permissions = new List<Permission>
            {
                new Permission
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    PermissionTypeId = 1,
                    PermissionDate = new DateOnly(2024, 8, 15),
                    PermissionType = new PermissionType { Id = 1, Description = "Admin", Name = "Admin" }
                }
            };
            var permissionDtos = new List<PermissionDto>
            {
                new PermissionDto
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
                }
            };
            Mock.Arrange(() => _mockUnitOfWork.Permissions.GetPermissionTypesWithPermissionsAsync()).ReturnsAsync(permissionDtos);
            Mock.Arrange(() => _mockMapper.Map<IEnumerable<PermissionDto>>(permissions)).Returns(permissionDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.AreEqual(permissionDtos, result);
        }
    }
}
