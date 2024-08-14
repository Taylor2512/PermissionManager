using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Models;
using PermissionManager.Core.Services.Dtos;

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
        public async Task<IActionResult> RequestPermission(PermissionRequest permission)
        {
            await _permissionService.RequestPermissionAsync(permission);
            return Ok(permission);
        }

        [HttpPut("modify/{id}")]
        public async Task<IActionResult> ModifyPermission(int id,PermissionRequest permission)
        {
            await _permissionService.ModifyPermissionAsync(id,permission);
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
