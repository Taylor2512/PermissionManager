using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PermissionManager.Core.Models;

namespace PermissionManager.API.Controllers
{
    [Route("api/permissiontypes")]
    [ApiController]
    public class PermissionTypeCntroller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPermissionTypes()
        {
            var permissionTypes = new[]
            {
                new PermissionType { Id = 1, Name = "Read", Description = "Allows read-only access" },
                new PermissionType { Id = 2, Name = "Write", Description = "Allows writing and modifying data" },
                new PermissionType { Id = 3, Name = "Execute", Description = "Allows executing operations" },
                new PermissionType { Id = 4, Name = "Delete", Description = "Allows deletion of data" },
                new PermissionType { Id = 5, Name = "Admin", Description = "Grants full administrative access" }
            };
            return Ok(permissionTypes);
        }
    }
}
