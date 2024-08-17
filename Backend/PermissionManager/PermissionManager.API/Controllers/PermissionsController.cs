using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PermissionManager.Core.Interfaces;

using PermissionManager.Core.Services.Request;

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

        [HttpPost]
        public async Task<IActionResult> RequestPermission(PermissionRequest permission)
        {
            await _permissionService.RequestPermissionAsync(permission);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPermission(int id, PermissionRequest permission)
        {
            await _permissionService.ModifyPermissionAsync(id, permission);
            return Ok(permission);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionService.GetPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionsById(int id)
        {
            var permissions = await _permissionService.GetPermissionByIdAsync(id);
            return Ok(permissions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionAsync(int id)
        {
            await _permissionService.DeletePermissionAsync(id);
            return NoContent();
        }
    }
}
