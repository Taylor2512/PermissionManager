using Microsoft.VisualStudio.TestTools.UnitTesting;

using PermissionManager.API.Controllers;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services.Request;

using System;
using System.Threading.Tasks;

using Telerik.JustMock;
using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Services.Dtos;
using System.Collections.Generic;
using Xunit;



namespace PermissionManager.Tests.Controllers
{
    [TestClass]
    public class PermissionsControllerTests
    {
        private readonly IPermissionService _mockPermissionService;
        private readonly PermissionsController _controller;

        public PermissionsControllerTests()
        {
            _mockPermissionService = Mock.Create<IPermissionService>();
            _controller = new PermissionsController(_mockPermissionService);
        }

        [TestMethod]
        public async Task RequestPermission_ReturnsCreatedResult()
        {
            var request = new PermissionRequest
            {
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionTypeId = 1
            };
            Mock.Arrange(() => _mockPermissionService.RequestPermissionAsync(request)).Returns(Task.CompletedTask);

            var result = await _controller.RequestPermission(request);

            Assert.IsInstanceOfType<CreatedResult>(result);
        }
   

        [TestMethod]
        public async Task ModifyPermission_ReturnsOkResult()
        {
            var request = new PermissionRequest
            {
                EmployeeForename = "Jane",
                EmployeeSurname = "Doe",
                PermissionTypeId = 1
            };
            var id = 1;
            Mock.Arrange(() => _mockPermissionService.ModifyPermissionAsync(id, request)).Returns(Task.CompletedTask);

            var result = await _controller.ModifyPermission(id, request);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetPermissions_ReturnsOkResult_WithPermissions()
        {
            var permissions = new List<PermissionDto>
            {
                new PermissionDto
                {
                    Id = 1,
                    EmployeeForename = "John",
                    EmployeeSurname = "Doe",
                    PermissionTypeName = "Admin",
                    PermissionDate = DateTime.UtcNow,
                    PermissionType = new PermissionTypeDto
                    {
                        Id = 1,
                        Description = "Administrative permissions",
                        Name = "Admin"
                    }
                }
            };
            Mock.Arrange(() => _mockPermissionService.GetPermissionsAsync()).ReturnsAsync(permissions);

            var result = await _controller.GetPermissions();
            Assert.IsInstanceOfType<IActionResult>(result);
            var results = (OkObjectResult)result;
            Assert.AreEqual(permissions, results.Value);
        }

        [TestMethod]
        public async Task GetPermissionsById_ReturnsOkResult_WithPermission()
        {
            var id = 1;
            var permission = new PermissionDto
            {
                Id = 1,
                EmployeeForename = "Jane",
                EmployeeSurname = "Doe",
                PermissionTypeName = "User",
                PermissionDate = DateTime.UtcNow,
                PermissionType = new PermissionTypeDto
                {
                    Id = 2,
                    Description = "User permissions",
                    Name = "User"
                }
            };
            Mock.Arrange(() => _mockPermissionService.GetPermissionByIdAsync(id)).ReturnsAsync(permission);

            var result = await _controller.GetPermissionsById(id);

            Assert.IsInstanceOfType<OkObjectResult>(result);
            var results = (OkObjectResult)result;
            Assert.AreEqual(permission, results.Value);
        }

        [TestMethod]
        public async Task DeletePermission_ReturnsNoContentResult()
        {
            var id = 1;
            Mock.Arrange(() => _mockPermissionService.DeletePermissionAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.DeletePermissionAsync(id);

            Assert.IsInstanceOfType<NoContentResult>(result);
        }
    }
}