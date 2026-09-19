using Business_Layer.DTOs.AccessPolicies;
using Business_Layer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessPolicyController : ControllerBase
    {
        private readonly IAccessPolicyService _service;

        public AccessPolicyController(IAccessPolicyService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult>
            CreatePolicy(AccessPolicyDto dto)
        {
            return Ok(await _service.CreatePolicy(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult>
            UpdatePolicy(AccessPolicyDto dto)
        {
            return Ok(await _service.UpdatePolicy(dto));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult>
            DeletePolicy(int id)
        {
            return Ok(await _service.DeletePolicy(id));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>
            GetPolicies()
        {
            return Ok(await _service.GetPolicies());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>
            GetPolicyById(int id)
        {
            return Ok(await _service.GetPolicyById(id));
        }
    }
}
