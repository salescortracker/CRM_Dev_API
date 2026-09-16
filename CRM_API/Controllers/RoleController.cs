using Business_Layer.DTOs.Roles;
using Business_Layer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult>
            CreateRole(RoleDto dto)
        {
            return Ok(await _service.CreateRole(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult>
            UpdateRole(RoleDto dto)
        {
            return Ok(await _service.UpdateRole(dto));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult>
            DeleteRole(int id)
        {
            return Ok(await _service.DeleteRole(id));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>
            GetRoles()
        {
            return Ok(await _service.GetRoles());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>
            GetRoleById(int id)
        {
            return Ok(await _service.GetRoleById(id));
        }
    }
}
