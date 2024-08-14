using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;

namespace PermissionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPermission(Permission permission)
        {
            await _permissionService.RequestPermissionAsync(permission);
            return Ok(permission);
        }

        [HttpPut("modify")]
        public async Task<IActionResult> ModifyPermission(Permission permission)
        {
            await _permissionService.ModifyPermissionAsync(permission);
            return Ok(permission);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionService.GetPermissionsAsync();
            return Ok(permissions);
        }
    }
}
